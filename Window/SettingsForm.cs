using AI.Provincial.Evolution;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using AI.Provincial.PlayAgenda;
using GameCore.Cards;
using System.Linq;
using GameCore;

namespace Window
{
    public partial class SettingsForm : Form
    {
        List<Card> kingdom;
        AIResult aipar;
        public SettingsForm(List<Card> kingdom, AIResult aipar)
        {
            this.kingdom = kingdom.AddRequiredCards();
            this.aipar = aipar;
            InitializeComponent();
        }

        private void Return(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ProvincialShow(object sender, EventArgs e)
        {
            // todo label napis neco jako toto kingdom jeste nema vygenerovanou inteligenci
            var agenda = BuyAgenda.Load(kingdom);
            if (agenda == null)
                agenda = BuyAgenda.GetRandom(kingdom);
        }

        private void Run(object sender, EventArgs e)
        {
            var evolution = new Evolution(new Params { Kingdom = kingdom}); // todo neco s timto
            evolution.Run();
        }
    }
}
