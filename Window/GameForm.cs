using System;
using System.Collections.Generic;
using System.Threading;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using GameCore;
using GameCore.Cards;
using System.Threading.Tasks;
using System.IO;
using Utils;
using AI.Provincial;
using AI.Model;

namespace Window
{
    public partial class GameForm : Form
    {
        GameParams gameParams = GameParams.Load();

        Job job = new Job();
        int min, max;
        const int dy = 30, dx = 145;
        static readonly char sep = Path.DirectorySeparatorChar;
        string directoryPath = $"..{sep}..{sep}..{sep}AI{sep}Provincial{sep}data{sep}kingdoms{sep}";
        CancellationTokenSource tokenSource = new CancellationTokenSource();

        public GameForm()
        {
            InitializeComponent();
            MaximumSize = Size;
            MinimumSize = Size;
            SettingsPanel.Parent = this;
            SettingsPanel.Location = new Point(0, 36);

            PlayerNameTextBox.Text = gameParams.User1Name;

            switch (gameParams.AIType)
            {
                case AIType.Tens:
                    TensRadio.Checked = true;
                    break;
                case AIType.Fives:
                    FivesRadio.Checked = true;
                    break;
                case AIType.Threes:
                    ThreesRadio.Checked = true;
                    break;
                default:
                    break;
            }

            SetKingdom_Click(null, null);
        }

        #region Game 

        void StartButton_Click(object sender, EventArgs e)
        {
            GamePanel.Show();
            SetKingdomPanel.Hide();
            SettingsPanel.Hide();
            LogTextBox.Text = "";
            StartGameButton.Text = "Restart";
            SetKingdomButton.Enabled = false;
            SettingsButton.Enabled = false;
            PlayAreaPanel.Controls.Clear();
            PhaseLabel.Text = "Loading";
            PhaseDescription.Text = "Please wait, game will be ready as soon as possible.";

            gameParams.Save();

            if (tokenSource != null)
            {
                tokenSource.Cancel();
                tokenSource = new CancellationTokenSource();
            }

            Task.Run(() =>
            {
                var human = new Human(PlayCard, GainCard, Choice, AlternativeChoice, job, gameParams.User1Name);
                BuyAgendaManager manager = null;

                switch (gameParams.AIType)
                {
                    case AIType.Tens:
                        manager = new SimpleManager(directoryPath, "Tens_");
                        break;
                    case AIType.Fives:
                        manager = new CachedManager(directoryPath, 5, "Fives_");    
                        break;
                    case AIType.Threes:
                        manager = new CachedManager(directoryPath, 3, "Threes_");
                        break;
                    default:
                        break;
                }

                var agenda = manager.LoadBest(gameParams.Cards);
                if (agenda == null)
                {
                    Action function = () =>
                    {
                        MessageBox.Show("There is no suitable opponent for this kingdom. \n Please try different cards or" +
                          " different type of opponent.");
                        StopButton_Click(sender, e);
                    };
                    this.Invoke(function);
                    return;
                }

                var ai = new ProvincialAI(agenda, gameParams.AIType.ToString());

                var source = new CancellationTokenSource();

                Game game = new Game(new User[] { ai, human }, gameParams.Cards.GetKingdom(2), new WindowLogger(Log), tokenSource);
                game.Play(int.MaxValue).ContinueWith((results) => EnableNextGame(results));
            });
        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            StartGameButton.Text = "Start";
            SetKingdomButton.Enabled = true;
            SettingsButton.Enabled = true;

            if (tokenSource != null)
                tokenSource.Cancel();

            SetKingdom_Click(sender, e);
        }

        void EnableNextGame(Task<GameResults> results)
        {
            Action<GameResults> function = (gr) =>
            {
                ShowKingdom(gr.Players[0].Game.Kingdom);
                SetKingdomButton.Enabled = true;
                SettingsButton.Enabled = true;
            };
            this.Invoke(function, new object[] { results.Result });
        }

        void PlayCard(IEnumerable<Card> c, PlayerState s, Kingdom k, Phase p, Card a)
        {
            Action<IEnumerable<Card>, PlayerState, Kingdom, Phase, Card> function = (cards, ps, kingdom, phase, attackCard) =>
            {
                RefreshWindow(ps, kingdom, phase, attackCard);

                int y = 0, x = 0;
                foreach (var card in cards.OrderBy(b => b.Price).ThenBy(b => b.Name))
                {
                    var button = new Button()
                    {
                        Text = card.Name + (phase == Phase.Buy ? " $" + card.Price.ToString() : string.Empty),
                        Location = new Point(3 + x * dx, y += (x == 0 ? dy : 0)),
                        Tag = card,
                        Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                        ForeColor = Color.DarkGray,
                        BackColor = card.ToBackColor(),
                        Width = 138,
                        Height = 25,
                        UseVisualStyleBackColor = false,
                        FlatStyle = FlatStyle.Flat,
                        FlatAppearance = { BorderColor = Color.DarkGray }
                    };
                    x = x == 0 ? 1 : 0;

                    // selecting which card can be played (or bought)
                    if (phase == Phase.Action && card.IsAction ||
                        phase == Phase.Treasure && card.IsTreasure ||
                        phase == Phase.Reaction && card.IsReaction ||
                        phase == Phase.Buy ||
                        phase == Phase.Gain)
                    {
                        button.Click += SelectDecision;
                        button.ForeColor = Color.Black;
                    }

                    PlayAreaPanel.Controls.Add(button);
                }
                AddDoneButton(ref y, SelectDecision);
            };

            this.Invoke(function, new object[] {p == Phase.Buy || p == Phase.Gain ? c : s.Hand, s, k, p, a});
        }
        
        void GainCard(IEnumerable<Card> cards, PlayerState ps, Kingdom k, Phase phase)
        {
            PlayCard(cards, ps, k, phase, null);
        }

        void Choice(IEnumerable<Card> c, PlayerState state, Kingdom k, int mininum, int maximum, Phase p, Card a)
        {
            Action<IEnumerable<Card>, PlayerState, Kingdom, int, int, Phase, Card> function = (cards, ps, kingdom, min, max, phase, attackCard) =>
            {
                this.min = min;
                this.max = max;
                RefreshWindow(ps, kingdom, phase, attackCard);

                int y = 8, x = 0;
                foreach (var card in cards.OrderBy(b => b.Name).OrderBy(b => b.Price))
                {
                    var checkBox = new CheckBox()
                    {
                        Location = new Point(5 + x * dx, y += (x == 0 ? 1 : 0) * dy),
                        Tag = card,
                        Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                    };

                    var label = new Label()
                    {
                        Text = $"{card.Name}",
                        Location = new Point(33 + x * 145, y),
                        Size = new Size(108, 23),
                        Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                        BackColor = card.ToBackColor(),
                        TextAlign = ContentAlignment.MiddleCenter,
                        FlatStyle = FlatStyle.Flat,
                        BorderStyle = BorderStyle.FixedSingle,
                    };
                   
                    x = x == 0 ? 1 : 0;

                    PlayAreaPanel.Controls.Add(label);
                    PlayAreaPanel.Controls.Add(checkBox);
                }

                AddDoneButton(ref y, SelectCardSet);
            };

            this.Invoke(function, new object[] { c, state, k, mininum, maximum, p, a });
        }

        void RefreshWindow(PlayerState ps, Kingdom kingdom, Phase phase, Card card)
        {
            ShowKingdom(kingdom);

            ActionLabel.Text = $"Actions: {ps.Actions}";
            BuyLabel.Text = $"Buys: {ps.Buys}";
            CoinLabel.Text = $"Coins: {ps.Coins}";

            // clearing old card buttons
            PlayAreaPanel.Controls.Clear();
            PlayAreaPanel.Controls.Add(PlayAreaLabel);

            switch (phase)
            {
                case Phase.Action:
                    PhaseLabel.Text = ps.Name + ": Action phase";
                    PhaseDescription.Text = "Select an action card to play.";
                    PhasePanel.BackColor = Color.FromArgb(255, 227, 227, 227);
                    PlayAreaLabel.Text = "Hand";
                    break;
                case Phase.Treasure:
                    PhaseLabel.Text = ps.Name + ": Buy phase";
                    PhaseDescription.Text = "Select a treasure to play.";
                    PhasePanel.BackColor = Color.Yellow;
                    PlayAreaLabel.Text = "Treasures";
                    break;
                case Phase.Buy:
                    PhaseLabel.Text = ps.Name + ": Buy phase";
                    PhaseDescription.Text = "Buy card.";
                    PhasePanel.BackColor = Color.SandyBrown;
                    PlayAreaLabel.Text = "Cards to buy";
                    break;
                case Phase.Gain:
                    PhaseLabel.Text = ps.Name + ": Gain phase";
                    PhaseDescription.Text = "Gain card without buying it.";
                    PhasePanel.BackColor = Color.SandyBrown;
                    PlayAreaLabel.Text = "Cards to gain";
                    break;
                case Phase.Reaction:
                    PhaseLabel.Text = ps.Name + ": Reaction phase";
                    PhaseDescription.Text = $"Card {card.Name} was played. You can play some reaction cards.";
                    PhasePanel.BackColor = Color.LightBlue;
                    PlayAreaLabel.Text = "Hand";
                    break;
                case Phase.Attack:
                    PhaseLabel.Text = ps.Name + ": Attack";
                    PhasePanel.BackColor = Color.Red;
                    PlayAreaLabel.Text = $"{card?.Message}";
                    break;
                default:
                    throw new NotSupportedException();
            }

            if (card != null)
                PhaseDescription.Text = card.Message;
        }

        void AddDoneButton(ref int y, EventHandler eventHandler)
        {
            var button = new Button()
            {
                Text = $"Done",
                Location = new Point(80, y += dy),
                Size = new Size(138, 25),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                BackColor = Color.DarkGray,
            };
            button.FlatAppearance.BorderSize = 0;
            button.FlatStyle = FlatStyle.Flat;
            button.Click += eventHandler;
            PlayAreaPanel.Controls.Add(button);
        }

        void SelectDecision(object sender, EventArgs e)
        {
            lock (job)
            {
                job.Result = (sender as Control).Tag;
                job.Done = true;
                Monitor.PulseAll(job);
                PlayAreaPanel.Controls.Clear();
            }
        }

        void SelectCardSet(object sender, EventArgs e)
        {
            var cards = new List<Card>();
            foreach (var control in PlayAreaPanel.Controls)
                if (control is CheckBox && (control as CheckBox).Checked)
                    cards.Add((Card)(control as CheckBox).Tag);

            if (cards.Count < min || cards.Count > max)
            {
                MessageBox.Show($"You cannot select less than {min} or more than {max} cards.");
                return;
            }

            lock (job)
            {
                job.Result = cards;
                job.Done = true;
                // pulse wont work if you press restart game
                Monitor.PulseAll(job);
                PlayAreaPanel.Controls.Clear();
            }
        }

        void ShowKingdom(Kingdom kingdom)
        {
            KingdomPanel.Controls.Clear();
            KingdomPanel.Controls.Add(KingdomLabel);

            for (int i = 0; i < kingdom.Count(); i++)
            {
                KingdomPanel.Controls.Add(new Label
                {
                    Text = kingdom[i].ToString(),
                    Location = new Point(3, 38 + i * 20),
                    Size = new Size(142, 17),
                    Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                    BackColor = kingdom[i].Card.ToBackColor(),
                    TextAlign = ContentAlignment.MiddleCenter,
                });
            }
        }

        void AlternativeChoice(PlayerState s, Kingdom k, Phase p, Card a, string y, string n)
        {
            Action<PlayerState, Kingdom, Phase, Card, string, string> function = (ps, kingdom, phase, attackCard, yup, nay) =>
            {
                RefreshWindow(ps, kingdom, phase, attackCard);

                var trueButton = new Button()
                {
                    Text = yup,
                    Location = new Point(3, dy),
                    Tag = true,
                    Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                    ForeColor = Color.Black,
                    BackColor = Color.WhiteSmoke,
                    Width = 138,
                    Height = 25,
                    UseVisualStyleBackColor = false,
                    FlatStyle = FlatStyle.Flat,
                    FlatAppearance = { BorderColor = Color.DarkGray }
                };

                trueButton.Click += SelectDecision;
                PlayAreaPanel.Controls.Add(trueButton);

                var falseButton = new Button()
                {
                    Text = nay,
                    Location = new Point(3 + dx, dy),
                    Tag = false,
                    Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                    ForeColor = Color.Black,
                    BackColor = Color.WhiteSmoke,
                    Width = 138,
                    Height = 25,
                    UseVisualStyleBackColor = false,
                    FlatStyle = FlatStyle.Flat,
                    FlatAppearance = { BorderColor = Color.DarkGray }
                };

                falseButton.Click += SelectDecision;
                PlayAreaPanel.Controls.Add(falseButton);
            };

            this.Invoke(function, new object[] { s, k, p, a, y, n });
        }

        void SetVisible(Control button, bool visible)
        {
            if (button.InvokeRequired)
            {
                Action<Control, bool> d = new Action<Control, bool>(SetVisible);
                this.Invoke(d, new object[] { button, visible });
            }
            else
            {
                button.Visible = visible;
            }
        }

        public void Log(string str)
        {
            Action<string> function = s => LogTextBox.AppendText(s);
            this.Invoke(function, new object[] { str });
        }

        #endregion

        #region Set kingdom

        void SetKingdom_Click(object sender, EventArgs e)
        {
            StartGameButton.Text = "Start";
            SetKingdomPanel.Show();
            GamePanel.Hide();
            SettingsPanel.Hide();
            ShowCurrentKingdomCards();
            ShowExtensionCards();
        }

        void ShowExtensionCards()
        {
            ExtensionsCardsPanel.Controls.Clear();
            ExtensionsCardsPanel.Controls.Add(ExtensionsCardsLabel);

            var differentCards = PresetGames.Get(Games.AllCards1stEdition).Where(c => !gameParams.Cards.Contains(c));
            var nec = differentCards.ToList();

            int y = 5, x = 0;
            foreach (var card in differentCards.OrderBy(a => a.Name).OrderBy(a => a.Price))
            {
                var button = new Button()
                {
                    Text = $"{card.Name} ${card.Price.ToString()}",
                    Location = new Point(55 + x * dx, y += (x == 0 ? dy : 0)),
                    Tag = card,
                    Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                    ForeColor = Color.Black,
                    BackColor = card.ToBackColor(),
                    Width = 138,
                    Height = 25,
                    UseVisualStyleBackColor = false,
                    FlatStyle = FlatStyle.Flat,
                    FlatAppearance = { BorderColor = Color.DarkGray }
                };

                x = x == 0 ? 1 : 0;
                button.Click += AddToKingdom;
                ExtensionsCardsPanel.Controls.Add(button);
            }
        }

        void AddToKingdom(object sender, EventArgs e)
        {
            gameParams.Cards.Add((sender as Control)?.Tag as Card);
            ShowCurrentKingdomCards();
            ShowExtensionCards();
        }

        void ShowCurrentKingdomCards()
        {
            gameParams.Cards = gameParams.Cards.OrderBy(a => a.Price).ThenBy(a => a.Name).ToList();

            CurrentKingdomPanel.Controls.Clear();
            CurrentKingdomPanel.Controls.Add(CurrentKingdomLabel);

            var bannedTypes = new []{ CardType.Copper, CardType.Silver, CardType.Gold, CardType.Estate, CardType.Duchy, CardType.Province };
            var changableKingdom = gameParams.Cards.Where(k => !bannedTypes.Contains(k.Type));

            int y = 5;
            foreach (var card in changableKingdom.OrderBy(a => a.Price).ThenBy(a => a.Name))
            {
                var button = new Button()
                {
                    Text = card.Name + " $" + card.Price.ToString(),
                    Location = new Point(4, y += dy),
                    Tag = card,
                    Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                    BackColor = card.ToBackColor(),
                    Width = 138,
                    Height = 25,
                    UseVisualStyleBackColor = false,
                    FlatStyle = FlatStyle.Flat,
                    FlatAppearance = { BorderColor = Color.DarkGray }
                };

                button.Click += RemoveFromKingdom;

                CurrentKingdomPanel.Controls.Add(button);
            }
        }

        void RemoveFromKingdom(object sender, EventArgs e)
        {
            gameParams.Cards.Remove((sender as Control)?.Tag as Card);
            ShowCurrentKingdomCards();
            ShowExtensionCards();
        }

        #endregion

        #region Settings

        void SetPresetGame(object sender, EventArgs e)
        {
            gameParams.Cards = PresetGames.Get((Games)int.Parse((sender as Button).Tag as string));
            ShowCurrentKingdomCards();
            ShowExtensionCards();
        }

        void SetRandomGame(object sender, EventArgs e)
        {
            // get random 10 cards
            gameParams.Cards = Enumerable.Range((int)CardType.Adventurer, 25)
                .Select(t => ((t, r: new ThreadSafeRandom().NextDouble())))
                .OrderBy(a => a.r)
                .Take(10)
                .Select(((int type, double) a) => Card.Get((CardType)a.type))
                .OrderBy(a => a.Price)
                .ThenBy(a => a.Name)
                .ToList();

            ShowCurrentKingdomCards();
            ShowExtensionCards();
        }

        private void Radio_CheckedChanged(object sender, EventArgs e)
        {
            Enum.TryParse((sender as RadioButton).Text, out AIType aiType);
            gameParams.AIType = aiType;
        }

        private void PlayerNameTextBox_TextChanged(object sender, EventArgs e)
        {
            gameParams.User1Name = (sender as TextBox).Text;
        }

        void SetPrecomputedRandomGame(object sender, EventArgs e)
        {
            var manager = new SimpleManager(directoryPath, "Tens_");
            gameParams.Cards = manager.RandomKingdom();

            ShowCurrentKingdomCards();
            ShowExtensionCards();
        }

        void OpenSettings(object sender, EventArgs e)
        {
            SetKingdomPanel.Hide();
            GamePanel.Hide();
            SettingsPanel.Show();
        }
        #endregion
    }
}
