using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class Game : Form
    {
        //string player = "";
        int player;

        int[,] point = new int[2, 5];

        int[] pL = { 0, 0 };

        bool bot = false;
        bool game = true;

        int[,] win = {
            { 1, 2, 3 },
            { 4, 5, 6 },
            { 7, 8, 9 },
            { 1, 4, 7 },
            { 2, 5, 8 },
            { 3, 6, 9 },
            { 1, 5, 9 },
            { 3, 5, 7 }
        };

        public Game(bool botA)
        {
            bot = botA;
            InitializeComponent();
            Start();
        }

        private void ClickP(object sender, EventArgs e)
        {
            if (game)
            {
                string[] str = { "X", "O" };
                ((Button)sender).Text = str[player];
                ((Button)sender).Enabled = false;
                switch (((Button)sender).Name)
                {
                    case "p1":
                        point[player, pL[player]] = 1;
                        break;
                    case "p2":
                        point[player, pL[player]] = 2;
                        break;
                    case "p3":
                        point[player, pL[player]] = 3;
                        break;
                    case "p4":
                        point[player, pL[player]] = 4;
                        break;
                    case "p5":
                        point[player, pL[player]] = 5;
                        break;
                    case "p6":
                        point[player, pL[player]] = 6;
                        break;
                    case "p7":
                        point[player, pL[player]] = 7;
                        break;
                    case "p8":
                        point[player, pL[player]] = 8;
                        break;
                    case "p9":
                        point[player, pL[player]] = 9;
                        break;
                }
                pL[player]++;
                Updater();

            }
        }
        private void Start()
        {
            p1.Text = "";
            p2.Text = "";
            p3.Text = "";
            p4.Text = "";
            p5.Text = "";
            p6.Text = "";
            p7.Text = "";
            p8.Text = "";
            p9.Text = "";

            p1.Enabled = true;
            p2.Enabled = true;
            p3.Enabled = true;
            p4.Enabled = true;
            p5.Enabled = true;
            p6.Enabled = true;
            p7.Enabled = true;
            p8.Enabled = true;
            p9.Enabled = true;
            //player = "X";
            player = 0;

            label3.Text = "";
            label2.Text = symvol();
        }
        private void Updater()
        {
            bool checker = CheckWin();

            if (checker)
            {
                WriteData(symvol());
                label3.Text = "WIN: " + symvol();
                game = false;
            }
            if (pL[0] + pL[1] == 9 && !checker)
            {
                Deactive();
                WriteData("Нічія");
                label3.Text = "WIN: Нічія";
                game = false;
            }
            if (player == 0)
            {
                player = 1;
            }
            else player = 0;
            label2.Text = symvol();
            if (bot && player == 1 && pL[0] + pL[1] != 9 && !checker) BotSetPoint();
        }
        private string symvol()
        {
            if (player == 0)
            {
                return "X";
            }
            else
            {
                if (bot)
                {
                    return "PC";
                }
                else return "O";
            }
        }
        private bool CheckWin()
        {
            int acc;

            for (int i = 0; i < 8; i++)
            {
                acc = 0;
                for (int j = 0; j < 3; j++)
                {
                    for (int k = 0; k < pL[player]; k++)
                    {
                        if (point[player, k] == win[i, j]) acc++;
                    }
                }

                if (acc == 3)
                {
                    PrintWinC(i);
                    return true;

                }
            }
            return false;
        }
        private void PrintWinC(int i)
        {
            Deactive();
            for(int j = 0; j < 3; j++)
            {
                switch (win[i, j])
                {
                    case 1:
                        p1.Enabled = true;
                        p1.ForeColor = Color.Red;
                        break;
                    case 2:
                        p2.Enabled = true;
                        p2.ForeColor = Color.Red;
                        break;
                    case 3:
                        p3.Enabled = true;
                        p3.ForeColor = Color.Red;
                        break;
                    case 4:
                        p4.Enabled = true;
                        p4.ForeColor = Color.Red;
                        break;
                    case 5:
                        p5.Enabled = true;
                        p5.ForeColor = Color.Red;
                        break;
                    case 6:
                        p6.Enabled = true;
                        p6.ForeColor = Color.Red;
                        break;
                    case 7:
                        p7.Enabled = true;
                        p7.ForeColor = Color.Red;
                        break;
                    case 8:
                        p8.Enabled = true;
                        p8.ForeColor = Color.Red;
                        break;
                    case 9:
                        p9.Enabled = true;
                        p9.ForeColor = Color.Red;
                        break;
                }
            }
            
        }
        private void Deactive()
        {
            p1.Enabled = false;
            p2.Enabled = false;
            p3.Enabled = false;
            p4.Enabled = false;
            p5.Enabled = false;
            p6.Enabled = false;
            p7.Enabled = false;
            p8.Enabled = false;
            p9.Enabled = false;

            label1.Visible = false;
            label2.Visible = false;

        }
        private void WriteData(string winner)
        {
            string line = "(" + DateTime.Now + ")" + " X против ";
            if (bot)
            {
                line += "PC";
            }
            else line += "O";
            line += " Win: " + winner;

            StreamWriter f = new StreamWriter("config.txt", true);
            f.WriteLine(line);
            f.Close();
        }
        private void BotSetPoint()
        {
            //Random rnd = new Random();
            //Thread.Sleep(rnd.Next(2000, 3000));
            int step = Bot();

            if (step == 99) this.Close(); //Обработка ошибки

            switch (step)
            {
                case 1:
                    p1.Text = "O";
                    p1.Enabled = false;
                    break;
                case 2:
                    p2.Text = "O";
                    p2.Enabled = false;
                    break;
                case 3:
                    p3.Text = "O";
                    p3.Enabled = false;
                    break;
                case 4:
                    p4.Text = "O";
                    p4.Enabled = false;
                    break;
                case 5:
                    p5.Text = "O";
                    p5.Enabled = false;
                    break;
                case 6:
                    p6.Text = "O";
                    p6.Enabled = false;
                    break;
                case 7:
                    p7.Text = "O";
                    p7.Enabled = false;
                    break;
                case 8:
                    p8.Text = "O";
                    p8.Enabled = false;
                    break;
                case 9:
                    p9.Text = "O";
                    p9.Enabled = false;
                    break;

            }
            point[player, pL[player]] = step;
            pL[player]++;
            Updater();

        }
        private int Bot()
        {
            int acc;
            //Проверяем близько ли О к победе
            for(int i = 0; i < 8; i++)
            {
                acc = 0;
                for(int j = 0; j < 3; j++)
                {
                    for(int k = 0; k < pL[1]; k++)
                    {
                        if (win[i, j] == point[1, k]) acc++;
                    }
                }
                if(acc == 2)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        for (int k = 0; k < pL[1]; k++)
                        {
                            if (win[i, j] != point[1, k] && freePoint(win[i, j])) return win[i, j];
                        }
                    }
                }
            }
            //Мешаем Х
            for (int checker = 2; checker >= 1; checker--)
            {
                for (int i = 0; i < 8; i++)
                {
                    acc = 0;
                    for (int j = 0; j < 3; j++)
                    {
                        for (int k = 0; k < pL[0]; k++)
                        {
                            if (win[i, j] == point[0, k]) acc++;
                        }
                    }
                    if (acc == checker)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            for (int k = 0; k < pL[0]; k++)
                            {
                                if (win[i, j] != point[0, k] && freePoint(win[i, j])) return win[i, j];
                            }
                        }
                    }
                }
            }
            return 99;
        }
        private bool freePoint(int point)
        {
            switch (point)
            {
                case 1:
                    if (p1.Enabled) return true;
                    break;
                case 2:
                    if (p2.Enabled) return true;
                    break;
                case 3:
                    if (p3.Enabled) return true;
                    break;
                case 4:
                    if (p4.Enabled) return true;
                    break;
                case 5:
                    if (p5.Enabled) return true;
                    break;
                case 6:
                    if (p6.Enabled) return true;
                    break;
                case 7:
                    if (p7.Enabled) return true;
                    break;
                case 8:
                    if (p8.Enabled) return true;
                    break;
                case 9:
                    if (p9.Enabled) return true;
                    break;

            }
            return false;
        }
    }
}
