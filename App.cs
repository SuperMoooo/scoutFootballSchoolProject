using System;
using System.Drawing;
using System.Windows.Forms;
using static ProjetoFinalScout.App;
using static ProjetoFinalScout.Database;

namespace ProjetoFinalScout
{
    public partial class App : Form
    {
        public App()
        {
            InitializeComponent();

            LoginPage.Show();
            createAccountPage.Hide();
            PaginasControl.Hide();
        }


        //::::::::::::::::LOGIN PAGE::::::::::::::::
        private void btnLogin_MouseEnter(object sender, EventArgs e)
        {
            btnLogin.ForeColor = Color.Tan;

        }

        private void btnLogin_MouseLeave(object sender, EventArgs e)
        {
            btnLogin.ForeColor = Color.White;

        }

        private void createAccountLabel_MouseEnter(object sender, EventArgs e)
        {
            createAccountLabel.ForeColor = Color.LightSteelBlue;
        }

        private void createAccountLabel_MouseLeave(object sender, EventArgs e)
        {
            createAccountLabel.ForeColor = Color.White;
        }

        private void goBackToPerfilPage_Click(object sender, EventArgs e)
        {
            LoginPage.Hide();

        }

        private void passwordInput_Enter(object sender, EventArgs e)
        {
            passwordBorder.BackColor = Color.Tan;
            passwordInput.ForeColor = Color.Tan;
        }

        private void passwordInput_Leave(object sender, EventArgs e)
        {
            passwordBorder.BackColor = Color.White;
            passwordInput.ForeColor = Color.White;

        }

        private void usernameInput_Enter(object sender, EventArgs e)
        {
            usernameBorder.BackColor = Color.Tan;
            usernameInput.ForeColor = Color.Tan;
        }

        private void usernameInput_Leave(object sender, EventArgs e)
        {
            usernameBorder.BackColor = Color.White;
            usernameInput.ForeColor = Color.White;
        }

        private void createAccountLabel_Click(object sender, EventArgs e)
        {
            LoginPage.Hide();
            createAccountPage.Show();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string userName = usernameInput.Text;
            string password = passwordInput.Text;

            try
            {

                bool checkLogin = CheckLoginAccount(userName, password);
                if (checkLogin)
                {
                    LoginPage.Hide();
                    createAccountPage.Hide();
                    PaginasControl.Show();
                    PaginasControl.SelectedTab = HomePage;


                }
                else
                {
                    MessageBox.Show("Dados inválidos", "Erro");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        //::::::::::::::::END LOGIN PAGE::::::::::::::::

        //::::::::::::::::CREATE ACC PAGE::::::::::::::::

        private void goBackToLogin_Click(object sender, EventArgs e)
        {
            LoginPage.Show();
            createAccountPage.Hide();
            textEmailLength = 0;
            whereIsEmailIs = 0;
        }

        private void createAccName_Enter(object sender, EventArgs e)
        {
            createAccName.ForeColor = Color.Tan;
            creatAccBorderName.BackColor = Color.Tan;
        }

        private void createAccName_Leave(object sender, EventArgs e)
        {
            createAccName.ForeColor = Color.White;
            creatAccBorderName.BackColor = Color.White;
        }

        private void creatAccountMailInput_Enter(object sender, EventArgs e)
        {
            creatAccountMailInput.ForeColor = Color.Tan;
            creatAccBorderMail.BackColor = Color.Tan;
        }

        private void creatAccountMailInput_Leave(object sender, EventArgs e)
        {
            creatAccountMailInput.ForeColor = Color.White;
            creatAccBorderMail.BackColor = Color.White;
        }

        //VERIFY EMAIL
        public bool isEmail = false;
        public int textEmailLength = 0;
        public int whereIsEmailIs = 0;

        private void creatAccountMailInput_KeyDown(object sender, KeyEventArgs e)
        {
            string text = creatAccountMailInput.Text;
            char arroba = '@';

            foreach (char c in text)
            {
                if (c == arroba)
                {
                    creatAccountMailInput.ForeColor = Color.Tan;
                    creatAccBorderMail.BackColor = Color.Tan;
                    isEmail = true;
                    break;
                }
                else
                {
                    isEmail = false;
                    creatAccountMailInput.ForeColor = Color.Red;
                    creatAccBorderMail.BackColor = Color.Red;

                }
            }
        }



        private void createAccPass_Enter(object sender, EventArgs e)
        {
            createAccPass.ForeColor = Color.Tan;
            creatAccBorderPass.BackColor = Color.Tan;
        }

        private void createAccPass_Leave(object sender, EventArgs e)
        {
            createAccPass.ForeColor = Color.White;
            creatAccBorderPass.BackColor = Color.White;
        }

        private void creatAccConfirmPass_Enter(object sender, EventArgs e)
        {
            creatAccConfirmPass.ForeColor = Color.Tan;
            creatAccBorderConfirmPass.BackColor = Color.Tan;
        }

        private void creatAccConfirmPass_Leave(object sender, EventArgs e)
        {
            creatAccConfirmPass.ForeColor = Color.White;
            creatAccBorderConfirmPass.BackColor = Color.White;
        }

        private void cargosComboBox_Enter(object sender, EventArgs e)
        {
            cargosComboBox.ForeColor = Color.Tan;
        }

        private void cargosComboBox_Leave(object sender, EventArgs e)
        {
            cargosComboBox.ForeColor = Color.White;
        }

        private void cargosComboBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void createAccBtn_Click(object sender, EventArgs e)
        {
            string name = createAccName.Text;
            string email = creatAccountMailInput.Text;
            string password = createAccPass.Text;
            string confirmPassword = creatAccConfirmPass.Text;
            string cargo = cargosComboBox.Text;
            string team = createTeamComboBox.Text;

            if (name != null && email != null && password != null && confirmPassword != null && cargo != null && team != null && isEmail == true)
            {
                if (password == confirmPassword)
                {
                    CreateAccount(name, password, email, cargo, team);
                    LoginPage.Show();
                    createAccountPage.Hide();
                }
                else
                {
                    MessageBox.Show("Os dois campos da password têm de ser iguais!", "Erro");
                }
            }
            else
            {
                MessageBox.Show("Por favor, preencha todos os campos corretamente!", "Erro");
            }
        }


        //::::::::::::::::END CREATE ACC PAGE::::::::::::::::


        //::::::::::::::::APP::::::::::::::::

        private void logOutBtn()
        {
            LoginPage.Show();
            createAccountPage.Hide();
            PaginasControl.Hide();
        }

        private void logOut_Click_1(object sender, EventArgs e)
        {
            logOutBtn();
        }

        private void accountPage_Enter(object sender, EventArgs e)
        {
            labelPerfil.Text = PerfilNameGlobal.perfilNameToGet;
        }

        private void logOut_MouseEnter(object sender, EventArgs e)
        {
            logOut.ForeColor = Color.Gray;
        }

        private void logOut_MouseLeave(object sender, EventArgs e)
        {
            logOut.ForeColor = Color.Silver;
        }


        //::::::::::::ENTER TABS PAGE:::::::::::::
        private void PaginasControl_Enter(object sender, EventArgs e)
        {

            //TABELA JOGADORES
            playersShow.DataSource = GetAllPlayers();
            playersShow.Columns["id"].Visible = false;
            playersShow.Columns["altura"].Visible = false;
            playersShow.Columns["pe_dominante"].Visible = false;
            playersShow.Columns["nacionalidade"].Visible = false;
            playersShow.Columns["chute"].Visible = false;
            playersShow.Columns["passe"].Visible = false;
            playersShow.Columns["defesa"].Visible = false;
            playersShow.Columns["interessado"].Visible = false;
            playersShow.DefaultCellStyle.Font = new Font("Poppins", 15);
            playersShow.ColumnHeadersDefaultCellStyle.Font = new Font("Poppins", 19, FontStyle.Bold);


            //JOGADORES INTERESSADOS

            playersInterested.DataSource = GetAllInterestedPlayers();
            playersInterested.Columns["id"].Visible = false;
            playersInterested.Columns["altura"].Visible = false;
            playersInterested.Columns["pe_dominante"].Visible = false;
            playersInterested.Columns["nacionalidade"].Visible = false;
            playersInterested.Columns["chute"].Visible = false;
            playersInterested.Columns["passe"].Visible = false;
            playersInterested.Columns["defesa"].Visible = false;
            playersInterested.Columns["interessado"].Visible = false;
            playersInterested.DefaultCellStyle.Font = new Font("Poppins", 15);
            playersInterested.ColumnHeadersDefaultCellStyle.Font = new Font("Poppins", 19, FontStyle.Bold);

            //TABELA PROPOSALS

            receiveProposalsTable.DataSource = GetProposals(GetTeamUser(PerfilNameGlobal.perfilNameId));
            receiveProposalsTable.Columns["id"].Visible = false;
            receiveProposalsTable.Columns["equipa_do_jogador"].Visible = false;
            receiveProposalsTable.Columns["scoutId"].Visible = false;
            receiveProposalsTable.Columns["duração_contrato"].Visible = false;
            receiveProposalsTable.Columns["importancia"].Visible = false;
            receiveProposalsTable.Columns["salario"].Visible = false;
            receiveProposalsTable.DefaultCellStyle.Font = new Font("Poppins", 15);
            receiveProposalsTable.ColumnHeadersDefaultCellStyle.Font = new Font("Poppins", 19, FontStyle.Bold);




            //CONTA

            CheckAccount();

        }

        private void CheckAccount()
        {
            if (PerfilNameGlobal.perfilNameToGet == "Olheiro")
            {
                labelPlayersInterested.Text = "Jogadores Interessados--------------------";
                statisticPlayersInterested.Text = CountInterested().ToString();

            }
            else
            {
                labelPlayersInterested.Text = "Propostas Recebidas-----------------------";
                statisticPlayersInterested.Text = CountProposals(GetTeamUser(PerfilNameGlobal.perfilNameId)).ToString();
            }


            totalClubMoney.Text = GetMoneyTeam(GetTeamUser(PerfilNameGlobal.perfilNameId)).ToString();
            actualClub.Text = GetTeamUser(PerfilNameGlobal.perfilNameId).ToString();
        }

        private void teamsComboBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void positionComboBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void addPlayerBtn_Click(object sender, EventArgs e)
        {
            if (PerfilNameGlobal.perfilNameToGet == "Treinador")
            {
                try
                {
                    string name = playerName.Text;
                    string age = ageBox.Text;
                    string position = positionComboBox.SelectedItem.ToString();
                    string nationality = nationalityComboBox.SelectedItem.ToString();
                    string team = GetTeamUser(PerfilNameGlobal.perfilNameId).ToString();
                    string height = heightBox.Text.ToString();
                    string foot = footComboBox.SelectedItem.ToString();
                    string shoot = shootBox.Text.ToString();
                    string pass = passBox.Text.ToString();
                    string defense = defenseBox.Text.ToString();

                    if (name != null && age != null && team != null && position != null)
                    {

                        AddPlayer(name, position, age, nationality, team, height, foot, shoot, pass, defense);

                        playersShow.DataSource = GetAllPlayers();


                        playerName.Text = "";
                        ageBox.Value = 14;
                        positionComboBox.SelectedItem = null;
                        nationalityComboBox.SelectedItem = null;
                        heightBox.Value = 130;
                        footComboBox.SelectedItem = null;
                        shootBox.Value = 0;
                        passBox.Value = 0;
                        defenseBox.Value = 0;
                    }
                    else
                    {
                        MessageBox.Show("Por favor preencha todos os campos!", "Erro");
                    }
                }


                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "Erro");
                }
            }
            else
            {
                MessageBox.Show("Neste momento não tem permissões para adicionar jogadores na lista", "Erro");
            }
        }

        private void addPlayerBtn_MouseEnter(object sender, EventArgs e)
        {
            addPlayerBtn.ForeColor = Color.Wheat;
        }

        private void addPlayerBtn_MouseLeave(object sender, EventArgs e)
        {
            addPlayerBtn.ForeColor = Color.PaleGreen;
        }


        private void ageBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }





        private void playersShow_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            showPlayerName.Text = "";
            showPlayerAge.Text = "";
            showPlayerHeight.Text = "";
            showPlayerFoot.Text = "";
            showPlayerNationality.Text = "";
            showPlayerPosition.Text = "";
            showPlayerTeam.Text = "";
            showPlayerShoot.Text = "";
            showPlayerPass.Text = "";
            showPlayerDefense.Text = "";

            string name = playersShow.Rows[e.RowIndex].Cells["nome"].Value.ToString();
            string age = playersShow.Rows[e.RowIndex].Cells["idade"].Value.ToString();
            string team = playersShow.Rows[e.RowIndex].Cells["equipa"].Value.ToString();
            string position = playersShow.Rows[e.RowIndex].Cells["posicao"].Value.ToString();
            string height = playersShow.Rows[e.RowIndex].Cells["altura"].Value.ToString();
            string nationality = playersShow.Rows[e.RowIndex].Cells["nacionalidade"].Value.ToString();
            string foot = playersShow.Rows[e.RowIndex].Cells["pe_dominante"].Value.ToString();
            string shoot = playersShow.Rows[e.RowIndex].Cells["chute"].Value.ToString();
            string pass = playersShow.Rows[e.RowIndex].Cells["passe"].Value.ToString();
            string defense = playersShow.Rows[e.RowIndex].Cells["defesa"].Value.ToString();

            showPlayerName.Text = name;
            showPlayerAge.Text = age;
            showPlayerHeight.Text = height;
            showPlayerFoot.Text = foot;
            showPlayerNationality.Text = nationality;
            showPlayerPosition.Text = position;
            showPlayerTeam.Text = team;
            showPlayerShoot.Text = shoot;
            showPlayerPass.Text = pass;
            showPlayerDefense.Text = defense;
        }

        private void heightBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void defenseBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void passBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void shootBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void interessePlayerBtn_Click_1(object sender, EventArgs e)
        {
            string name = showPlayerName.Text;
            string age = showPlayerAge.Text;
            string team = showPlayerTeam.Text;
            string position = showPlayerPosition.Text;
            string height = showPlayerHeight.Text;

            if (PerfilNameGlobal.perfilNameToGet == "Olheiro")
            {
                if (showPlayerName.Text != null && showPlayerAge.Text != null && showPlayerTeam.Text != null && showPlayerPosition.Text != null && showPlayerHeight.Text != null)
                {
                    int playerId = GetPlayerId(name, age, position, team, height);
                    if(GetTeamPlayer(playerId) == GetTeamUser(PerfilNameGlobal.perfilNameId))
                    {
                        MessageBox.Show("Não pode se interessar por um jogador da sua equipa", "Erro");
                    }
                    else
                    {
                        AddInterestedPlayer(playerId);
                        playersInterested.DataSource = GetAllInterestedPlayers();
                        playersShow.DataSource = GetAllPlayers();
                    }
                    
                }
            }
            else
            {
                MessageBox.Show("Neste momento não tem permissões para se interessar por jogadores da lista", "Erro");
            }
        }

        private void interessePlayerBtn_MouseEnter_1(object sender, EventArgs e)
        {
            interessePlayerBtn.ForeColor = Color.White;
        }

        private void interessePlayerBtn_MouseLeave_1(object sender, EventArgs e)
        {
            interessePlayerBtn.ForeColor = Color.BurlyWood;
        }



        private void deletePlayerBtn_Click_1(object sender, EventArgs e)
        {
            if (PerfilNameGlobal.perfilNameToGet == "Treinador")
            {
                string name = showPlayerName.Text;
                string age = showPlayerAge.Text;
                string team = showPlayerTeam.Text;
                string position = showPlayerPosition.Text;
                string height = showPlayerHeight.Text;

                try
                {
                    if (team == GetTeamUser(PerfilNameGlobal.perfilNameId))
                    {
                        if (showPlayerName.Text != null && showPlayerAge.Text != null && showPlayerTeam.Text != null && showPlayerPosition.Text != null && showPlayerHeight.Text != null)
                        {
                            DeletePlayer(name, position, age, team, height);
                            MessageBox.Show("O jogador foi removido!", "Sucesso");
                            playersShow.DataSource = GetAllPlayers();



                            showPlayerName.Text = "";
                            showPlayerAge.Text = "";
                            showPlayerHeight.Text = "";
                            showPlayerFoot.Text = "";
                            showPlayerNationality.Text = "";
                            showPlayerPosition.Text = "";
                            showPlayerTeam.Text = "";
                            showPlayerShoot.Text = "";
                            showPlayerPass.Text = "";
                            showPlayerDefense.Text = "";

                        }
                        else
                        {
                            MessageBox.Show("Por favor selecione um jogador antes de eliminar!", "Erro");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Este jogador não pertence à sua equipa", "Erro");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "Erro");
                }
            }
            else
            {
                MessageBox.Show("Neste momento não tem permissões para eliminar jogadores da lista", "Erro");
            }
        }

        private void deletePlayerBtn_MouseEnter_1(object sender, EventArgs e)
        {
            deletePlayerBtn.ForeColor = Color.White;
        }

        private void deletePlayerBtn_MouseLeave_1(object sender, EventArgs e)
        {
            deletePlayerBtn.ForeColor = Color.LightCoral;
        }

        private void createTeamComboBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void removeInteress_MouseEnter(object sender, EventArgs e)
        {
            interessePlayerBtn.ForeColor = Color.White;
        }

        private void removeInteress_MouseLeave(object sender, EventArgs e)
        {
            interessePlayerBtn.ForeColor = Color.BurlyWood;
        }

        private void sendProposta_MouseEnter(object sender, EventArgs e)
        {
            interessePlayerBtn.ForeColor = Color.White;
        }

        private void sendProposta_MouseLeave(object sender, EventArgs e)
        {
            interessePlayerBtn.ForeColor = Color.LightBlue;
        }

        private void contractDuration_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }


        private void importanceTeam_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }


        public static class playerInterested
        {
            public static int Id;
        }

        private void playersInterested_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            playerInterested.Id = (int)playersInterested.Rows[e.RowIndex].Cells["id"].Value;
        }

        public int lastPlayerIdGlobal = 0;

        private void removeInteress_Click(object sender, EventArgs e)
        {
            if (PerfilNameGlobal.perfilNameToGet == "Olheiro")
            {
                int playerId = playerInterested.Id;

                if (playerId != null)
                {

                    try
                    {
                        RemoveInterestedPlayer(playerId);
                        MessageBox.Show("Jogador removido da lista de interessados!", "Sucesso");
                        playersInterested.DataSource = GetAllInterestedPlayers();
                        playersShow.DataSource = GetAllPlayers();

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }


                }
                else
                {
                    MessageBox.Show("Por favor selecione um jogador antes de remover da lista!", "Erro");
                }
            }
            else
            {
                MessageBox.Show("Neste momento não tem permissões para mandar propostas a jogadores da lista", "Erro");
            }
        }


        private void sendProposta_Click(object sender, EventArgs e)
        {
            if (PerfilNameGlobal.perfilNameToGet == "Olheiro")
            {
                string contractYears = contractDuration.Value.ToString();
                string importance = importanceTeam.Text;
                string salarie = salario.Value.ToString();

                if (contractYears != null && importance != null && salarie != null)
                {
                    int playerId = playerInterested.Id;

                    if (playerId != null)
                    {
                        if (playerId != lastPlayerIdGlobal)
                        {
                            try
                            {
                                AddProposal(GetPlayerName(playerId), GetTeamPlayer(playerId), PerfilNameGlobal.perfilNameId, contractYears, importance, salarie, GetTeamUser(PerfilNameGlobal.perfilNameId));
                                lastPlayerIdGlobal = playerId;
                                playersShow.DataSource = GetAllPlayers();
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.ToString());
                            }
                        }
                        else
                        {
                            MessageBox.Show("Por favor tente mais tarde", "Erro");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Por favor selecione um jogador antes de remover da lista!", "Erro");
                    }
                }
            }
            else
            {
                MessageBox.Show("Neste momento não tem permissões para mandar propostas a jogadores da lista", "Erro");
            }

        }

        private void deleteAccount_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Isto ação irá eliminar todos os dados sobre esta conta, deseja continuar?", "Atenção", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                DeleteAccount(PerfilNameGlobal.perfilNameId);


                if (PerfilNameGlobal.perfilNameToGet == "Olheiro")
                {
                    DeleteAccountProposals(PerfilNameGlobal.perfilNameId);

                }
                MessageBox.Show("Conta eliminada com sucesso", "Sucesso");
                PaginasControl.Hide();
                LoginPage.Show();
            }
        }

        public static class proposal
        {
            public static int Id;
            public static int Salary;
            public static string TeamInterested;
            public static int ScoutId;
        }


        private void receiveProposalsTable_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            teamImportanceLabel.Text = "";
            salaryLabel.Text = "";
            durationContractLabel.Text = "";

            string duration = receiveProposalsTable.Rows[e.RowIndex].Cells["duração_contrato"].Value.ToString();
            string importance = receiveProposalsTable.Rows[e.RowIndex].Cells["importancia"].Value.ToString();
            string salarie = receiveProposalsTable.Rows[e.RowIndex].Cells["salario"].Value.ToString();
            proposal.Id = (int)receiveProposalsTable.Rows[e.RowIndex].Cells["id"].Value;
            proposal.Salary = int.Parse(receiveProposalsTable.Rows[e.RowIndex].Cells["salario"].Value.ToString());
            proposal.ScoutId = int.Parse(receiveProposalsTable.Rows[e.RowIndex].Cells["scoutId"].Value.ToString());
            proposal.TeamInterested = receiveProposalsTable.Rows[e.RowIndex].Cells["equipa_interessada"].Value.ToString();

            teamImportanceLabel.Text = importance;
            salaryLabel.Text = salarie;
            durationContractLabel.Text = duration;

        }
        
        private void acceptProposal_Click(object sender, EventArgs e)
        {
            if (PerfilNameGlobal.perfilNameToGet == "Treinador")
            {
                if (proposal.Id != 0)
                {
                    string Money = GetMoneyTeam(GetTeamUser(PerfilNameGlobal.perfilNameId));
                    int MoneyAux = int.Parse(Money);
                    int newMoney = MoneyAux + proposal.Salary;
                    //----------

                    string MoneyOtherTeam = GetMoneyTeam(GetTeamUser(proposal.ScoutId));
                    int MoneyAux2 = int.Parse(MoneyOtherTeam);
                    int removableMoney = MoneyAux2 - proposal.Salary;

                    AcceptProposal(proposal.Id, GetTeamUser(PerfilNameGlobal.perfilNameId), newMoney, removableMoney, proposal.TeamInterested);
                    MessageBox.Show("Proposta Aceite", "Sucesso");

                    

                    string playerName = GetPlayerByProposal(proposal.Id);

                    UpdateTeam(GetPlayerIdSimple(playerName), proposal.TeamInterested);

                    proposalsLastThingsToDo();
                    proposal.Id = 0;
                }
                else
                {
                    MessageBox.Show("Por favor selecione alguém da lista antes", "Erro");
                }


            }
            else
            {
                MessageBox.Show("Neste momento não tem permissões para tal", "Erro");
            }
        }

        private void removeProposal_Click(object sender, EventArgs e)
        {
            if (PerfilNameGlobal.perfilNameToGet == "Treinador")
            {
                if (proposal.Id != 0)
                {

                    MessageBox.Show("Proposta Recusada", "Sucesso");
                    proposalsLastThingsToDo();

                    proposal.Id = 0;
                }
                else
                {
                    MessageBox.Show("Por favor selecione alguém da lista antes", "Erro");
                }


            }
            else
            {
                MessageBox.Show("Neste momento não tem permissões para tal", "Erro");
            }

        }

        private void proposalsLastThingsToDo()
        {
            string playerName = GetPlayerByProposal(proposal.Id);
            RemoveInterestedPlayer(GetPlayerIdSimple(playerName));

            playersInterested.DataSource = GetAllInterestedPlayers();
            playersShow.DataSource = GetAllPlayers();

            DeleteSpecificProposal(proposal.Id);
            receiveProposalsTable.DataSource = GetProposals(GetTeamUser(PerfilNameGlobal.perfilNameId));
            CheckAccount();
        }




    }
}
