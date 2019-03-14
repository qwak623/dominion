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
        List<GameCore.Cards.Pile> kingdom;
        int min, max;

        public MyForm()
        {
            InitializeComponent();
        }

        void Form1_Load(object sender, EventArgs e)
        {
            kingdom = GameCore.Cards.Kingdom.TheFirstGame(true);
        }

        void StartGame_Click(object sender, EventArgs e)
        {
            GamePanel.Visible = true;
            LogTextBox.Text = "";
            var human = new Human(PlayCard, Choice, AlternativeChoice, job);
            var provincial = new Provincial();
            Game game = new Game(new User[] { human, provincial }, kingdom, new WindowLogger(Log));
            game.Run();
        }

        // todo mozna nejak systemove vyresit ktere karty se hraji a ktere ne
        void PlayCard(IEnumerable<Card> c, PlayerState s, Phase p)
        {
            Action<IEnumerable<Card>, PlayerState, Phase> function = (cards, ps, phase) => 
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
                        PlayAreaLabel.Text = "Hand";
                        break;
                    case Phase.Treasure:
                        PhaseLabel.Text = "Buy phase";
                        PhaseDescription.Text = "Select a treasure to play.";
                        PlayAreaLabel.Text = "Hand";
                        break;
                    case Phase.Buy:
                        PhaseLabel.Text = "Buy phase";
                        PhaseDescription.Text = "Buy card.";
                        PlayAreaLabel.Text = "Cards";
                        break;
                    default:
                        throw new NotSupportedException();
                }

                // todo tohle je nutné opravdu předělat jinak se to rozbije při resize
                int y = 8, x = 0;
                foreach (var card in cards.OrderBy(a => a.Name).OrderBy(a => a.Price))
                {
                    var button = new Button()
                    {
                        Text = card.Name + (phase == Phase.Buy ? " $" + card.Price.ToString() : string.Empty),
                        Tag = card,
                        Location = new Point(3 + x * 145, y += (x == 0 ? 1 : 0) * 28),
                        Size = new Size(138, 23),
                        Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                        ForeColor = Color.DarkGray,
                        BackColor = card.ToBackColor(),
                    };
                    button.FlatAppearance.BorderSize = 0;
                    button.FlatStyle = FlatStyle.Flat;
                    x = x == 0 ? 1 : 0;

                    // selecting which card can be played (or bought)
                    if (phase == Phase.Action && card.IsAction ||
                        phase == Phase.Treasure && card.IsTreasure ||
                        phase == Phase.Buy)
                    {
                        button.Click += SelectCard;
                        button.ForeColor = Color.Black;
                    }

                    PlayAreaPanel.Controls.Add(button);
                }
                AddDoneButton(ref y, SelectCard);
            };

            this.Invoke(function, new object[] {p == Phase.Buy ? c : s.Hand, s, p });
        }

        void Choice(IEnumerable<Card> c, PlayerState g, int mininum, int maximum)
        {
            Action<IEnumerable<Card>, PlayerState, int, int> function = (cards, gs, min, max) =>
            {
                ActionLabel.Text = $"Actions: {gs.Actions}";
                BuyLabel.Text = $"Buys: {gs.Buys}";
                CoinLabel.Text = $"Coins: {gs.Coins}";
                this.min = min;
                this.max = max;

                // clearing old card buttons
                PlayAreaPanel.Controls.Clear();
                PlayAreaPanel.Controls.Add(PlayAreaLabel);
                PhaseDescription.Text = min == max ? $"Select {min} cards." : $"Select {min} to {max} cards.";
                PlayAreaLabel.Text = "Choice";

                // todo tohle je nutné opravdu předělat jinak se to rozbije při resize
                int y = 8, x = 0;
                foreach (var card in cards.OrderBy(a => a.Name).OrderBy(a => a.Price))
                {
                    var checkBox = new CheckBox()
                    {
                        Location = new Point(5 + x * 145, y += (x == 0 ? 1 : 0) * 28),
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
                    };

                    x = x == 0 ? 1 : 0;

                    PlayAreaPanel.Controls.Add(label);
                    PlayAreaPanel.Controls.Add(checkBox);
                }

                AddDoneButton(ref y, SelectCardSet);
            };

            this.Invoke(function, new object[] { c, g, mininum, maximum });
        }

        void AddDoneButton(ref int y, EventHandler eventHandler)
        {
            var button = new Button()
            {
                Text = $"Done",
                Location = new Point(80, y += 28),
                Size = new Size(138, 23),
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
