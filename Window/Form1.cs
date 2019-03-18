using System;
using System.Collections.Generic;
using System.Threading;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using AI;
using GameCore;
using GameCore.Cards;

namespace Window
{
    public partial class MyForm : Form
    {
        Job job = new Job();
        Kingdom kingdom;
        int min, max;
        const int dy = 30, dx = 145;

        public MyForm()
        {
            InitializeComponent();
        }

        void Form1_Load(object sender, EventArgs e)
        {
            kingdom = GameCore.Cards.Kingdom.TheFirstGame();
        }

        void StartGame_Click(object sender, EventArgs e)
        {
            GamePanel.Visible = true;
            LogTextBox.Text = "";
            StartGame.Text = "Restart";
            var human = new Human(PlayCard, Choice, AlternativeChoice, job);
            var provincial = new Provincial();
            Game game = new Game(new User[] { human, provincial }, kingdom, new WindowLogger(Log));
            game.Run();
        }

        // todo mozna nejak systemove vyresit ktere karty se hraji a ktere ne
        void PlayCard(IEnumerable<Card> c, PlayerState s, Phase p, string cardName)
        {
            Action<IEnumerable<Card>, PlayerState, Phase, string> function = (cards, ps, phase, name) =>
            {
                RefreshWindow(ps, phase, name);

                

                // todo tohle je nutné opravdu předělat jinak se to rozbije při resize
                int y = 0, x = 0;
                foreach (var card in cards.OrderBy(a => a.Name).OrderBy(a => a.Price))
                {
                    var button = new Button()
                    {
                        Text = card.Name + (phase == Phase.Buy ? " $" + card.Price.ToString() : string.Empty),
                        Location = new Point(3 + x * dx, y += (x == 0 ? 1 : 0) * dy),
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
                        phase == Phase.Buy)
                    {
                        button.Click += SelectCard;
                        button.ForeColor = Color.Black;
                    }

                    PlayAreaPanel.Controls.Add(button);
                }
                AddDoneButton(ref y, SelectCard);
            };

            this.Invoke(function, new object[] {p == Phase.Buy ? c : s.Hand, s, p , cardName});
        }

        void Choice(IEnumerable<Card> c, PlayerState g, int mininum, int maximum, Phase p, string desc)
        {
            Action<IEnumerable<Card>, PlayerState, int, int, Phase, string> function = (cards, ps, min, max, phase, description) =>
            {
                this.min = min;
                this.max = max;
                RefreshWindow(ps, phase, null);
   
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

            this.Invoke(function, new object[] { c, g, mininum, maximum, p, desc });
        }

        private void RefreshWindow(PlayerState ps, Phase phase, string name)
        {
            ShowKingdom();

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

        void ShowKingdom()
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
    }
}
