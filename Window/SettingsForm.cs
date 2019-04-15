﻿using AI.Provincial.Evolution;
using AI.Trivial;
using AI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AI.Provincial.PlayAgenda;
using GameCore.Cards;
using GameCore;

namespace Window
{
    public partial class SettingsForm : Form
    {
        List<Card> kingdom;
        AIParams aipar;
        public SettingsForm(List<Card> kingdom, AIParams aipar)
        {
            this.kingdom = kingdom;
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
            aipar.User = new ProvincialAI(agenda);
        }

        private void Run(object sender, EventArgs e)
        {
            var evolution = new Evolution(new Params { Kingdom = kingdom}); // todo neco s timto
            evolution.Run(100);
        }
    }
}
