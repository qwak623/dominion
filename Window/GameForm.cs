using System;
using System.Collections.Generic;
using System.Threading;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using AI.Trivial;
using GameCore;
using GameCore.Cards;
using System.Threading.Tasks;

namespace Window
{
    public partial class GameForm : Form
    {
        List<Card> cards = NamedGames.Base.TheFirstGame();

        Job job = new Job();
        int min, max;
        const int dy = 30, dx = 145;
        AIParams aiParams = new AIParams();
        Task task;

        public GameForm()
        {
            InitializeComponent();
        }

        #region Game 

        void StartGame_Click(object sender, EventArgs e)
        {
            GamePanel.Show();
            SetKingdomPanel.Hide();
            LogTextBox.Text = "";
            StartGame.Enabled = false;  // todo konec hry nebo restart
            SetKingdom.Enabled = false;
            Settings.Enabled = false;

            var human = new Human(PlayCard, GainCard, Choice, AlternativeChoice, job);
            //var militial = new MilitialAI();
            var ai = aiParams.GetUser(cards);
            //var ai = new AI.Provincial.PlayAgenda.ProvincialAI(AI.Provincial.Evolution.BuyAgenda.GetRandom(cards));

            Game game = new Game(new User[] { human, ai}, cards.GetKingdom(true), new WindowLogger(Log));
            task = game.Play().ContinueWith((results) => EnableNextGame(results));
        }

        void EnableNextGame(Task<GameResults> results)
        {
            Action<GameResults> function = (gr) =>
            {
                ShowKingdom(gr.Players[0].Game.Kingdom);
                StartGame.Enabled = true;
                SetKingdom.Enabled = true;
                Settings.Enabled = true;
            };
            this.Invoke(function, new object[] { results.Result });
        }

        // todo mozna nejak systemove vyresit ktere karty se hraji a ktere ne
        void PlayCard(IEnumerable<Card> c, PlayerState s, Kingdom kingdom, Phase p, Card attackCard)
        {
            Action<IEnumerable<Card>, PlayerState, Kingdom, Phase, string> function = (cards, ps, k, phase, name) =>
            {
                RefreshWindow(ps, k, phase, name);

                // todo tohle je nutné opravdu předělat jinak se to rozbije při resize
                int y = 0, x = 0;
                foreach (var card in cards.OrderBy(a => a.Price).ThenBy(a => a.Name))
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
                        button.Click += SelectCard;
                        button.ForeColor = Color.Black;
                    }

                    PlayAreaPanel.Controls.Add(button);
                }
                AddDoneButton(ref y, SelectCard);
            };

            this.Invoke(function, new object[] {p == Phase.Buy || p == Phase.Gain ? c : s.Hand, s, kingdom, p, attackCard?.Destciption});
        }
        
        void GainCard(IEnumerable<Card> cards, PlayerState ps, Kingdom k, Phase phase)
        {
            PlayCard(cards, ps, k, phase, null);
        }

        void Choice(IEnumerable<Card> c, PlayerState state, Kingdom kingdom, int mininum, int maximum, Phase p, Card attackCard)
        {
            Action<IEnumerable<Card>, PlayerState, Kingdom, int, int, Phase, string> function = (cards, ps, k, min, max, phase, description) =>
            {
                this.min = min;
                this.max = max;
                RefreshWindow(ps, k, phase, null);
   
                if (description == null)
                    PhaseDescription.Text = min == max ? $"Select {min} cards." : $"Select {min} to {max} cards. ";
                PhaseDescription.Text = description;
                PlayAreaLabel.Text = "Choice";

                // todo tohle je nutné opravdu předělat jinak se to rozbije při resize
                int y = 8, x = 0;
                foreach (var card in cards.OrderBy(a => a.Name).OrderBy(a => a.Price))
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

            this.Invoke(function, new object[] { c, state, kingdom, mininum, maximum, p, attackCard?.Destciption });
        }

        void RefreshWindow(PlayerState ps, Kingdom kingdom, Phase phase, string name)
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
                    PhaseLabel.Text = "Action phase";
                    PhaseDescription.Text = "Select an action card to play.";
                    PhasePanel.BackColor = Color.FromArgb(255, 227, 227, 227);
                    PlayAreaLabel.Text = "Hand";
                    break;
                case Phase.Treasure:
                    PhaseLabel.Text = "Buy phase";
                    PhaseDescription.Text = "Select a treasure to play.";
                    PhasePanel.BackColor = Color.Yellow;
                    PlayAreaLabel.Text = "Treasures";
                    break;
                case Phase.Buy:
                    PhaseLabel.Text = "Buy phase";
                    PhaseDescription.Text = "Buy card.";
                    PhasePanel.BackColor = Color.SandyBrown;
                    PlayAreaLabel.Text = "Cards to buy";
                    break;
                case Phase.Gain:
                    PhaseLabel.Text = "Gain phase";
                    PhaseDescription.Text = "Gain card.";
                    PhasePanel.BackColor = Color.SandyBrown;
                    PlayAreaLabel.Text = "Cards to gain";
                    break;
                case Phase.Reaction:
                    PhaseLabel.Text = "Reaction phase";
                    PhaseDescription.Text = $"Card {name} was played. You can play some reaction cards.";
                    PhasePanel.BackColor = Color.LightBlue;
                    PlayAreaLabel.Text = "Hand";
                    break;
                case Phase.Attack:
                    PhaseLabel.Text = "Attack";
                    PhasePanel.BackColor = Color.Red;
                    PlayAreaLabel.Text = "Choice";
                    break;
                default:
                    throw new NotSupportedException();
            }
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

        void SelectCard(object sender, EventArgs e)
        {
            lock (job)
            {
                job.Result = (sender as Control).Tag;
                job.Done = true;
                Monitor.Pulse(job);
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
                Monitor.Pulse(job);
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

        void AlternativeChoice()
        {
            throw new NotImplementedException();
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
            SetKingdomPanel.Show();
            ShowCurrentKingdomCards();
            ShowExtensionCards();
        }

        void ShowExtensionCards()
        {
            ExtensionsCardsPanel.Controls.Clear();
            ExtensionsCardsPanel.Controls.Add(ExtensionsCardsLabel);

            var bannedTypes = new[] { CardType.Copper, CardType.Silver, CardType.Gold, CardType.Estate, CardType.Duchy, CardType.Province };
            var differentCards = NamedGames.Base.AllCards1stEdition().Where(c => !cards.Contains(c));

            var nec = differentCards.ToList();

            int y = 5;
            int x = 3;
            foreach (var card in differentCards.OrderBy(a => a.Name).OrderBy(a => a.Price))
            {
                var button = new Button()
                {
                    Text = $"{card.Name} ${card.Price.ToString()}",
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

                if (y > 15 * dy)
                {
                    y = 5;
                    x += dx;
                }

                button.Click += AddToKingdom;

                ExtensionsCardsPanel.Controls.Add(button);
            }
        }

        void AddToKingdom(object sender, EventArgs e)
        {
            cards.Add((sender as Control)?.Tag as Card);
            ShowCurrentKingdomCards();
            ShowExtensionCards();
        }

        void ShowCurrentKingdomCards()
        {
            CurrentKingdomPanel.Controls.Clear();
            CurrentKingdomPanel.Controls.Add(CurrentKingdomLabel);

            var bannedTypes = new []{ CardType.Copper, CardType.Silver, CardType.Gold, CardType.Estate, CardType.Duchy, CardType.Province };
            var changableKingdom = cards.Where(k => !bannedTypes.Contains(k.Type));

            int y = 5;
            foreach (var card in changableKingdom.OrderBy(a => a.Price).ThenBy(a => a.Name))
            {
                var button = new Button()
                {
                    Text = card.Name + " $" + card.Price.ToString(),
                    Location = new Point(3, y += dy),
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
            cards.Remove((sender as Control)?.Tag as Card);
            ShowCurrentKingdomCards();
            ShowExtensionCards();
        }

        #endregion

        #region Settings

        private void Settings_Click(object sender, EventArgs e)
        {
            var settingForm = new SettingsForm(cards, aiParams);
            settingForm.Show();
        }

        void Duel_Click(object sender, EventArgs e)
        {
            var sw = new System.Diagnostics.Stopwatch();
            sw.Start();

            for (int k = 0; k < 4; k++)
            {
                System.Threading.Tasks.Parallel.For(0, 100, i =>
                {
                    for (int j = 0; j < 100; j++)
                    {
                        var x = new AI.Provincial.PlayAgenda.ProvincialAI(AI.Provincial.Evolution.BuyAgenda.GetRandom(cards));
                        var y = new AI.Provincial.PlayAgenda.ProvincialAI(AI.Provincial.Evolution.BuyAgenda.GetRandom(cards));
                        //var m = new MilitialAI();
                        var got = new Game(new User[] { x, y }, cards.GetKingdom(true));
                        var task = got.Play();
                        var actualResults = task.Result;
                    }
                });
            }

            sw.Stop();
            var s = sw.Elapsed;
        }
        #endregion
    }
}
