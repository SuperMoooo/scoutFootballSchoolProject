using MySql.Data.MySqlClient;
using System.Data;
using System.Windows.Forms;

namespace ProjetoFinalScout
{
    public class Database
    {

        //::::::::::::::CONNECT DATABASE::::::::::::::::::
        public static MySqlConnection ConnectToMySql()
        {
            string sConn = "User Id=root;Password=;Server=localhost;Database=footSpace";
            MySqlConnection cn = new MySqlConnection(sConn);
            //cn.ConnectionString = sConn; // Properties.Settings.Default.connection;
            cn.Open();
            return cn;
        }


        //::::::::::::::CREATE ACCOUNT::::::::::::::::::
        public static void CreateAccount(string name, string password, string email, string cargo, string team)
        {
            var cn = ConnectToMySql();

            string createAccountQuery = "INSERT INTO usersfoot (nome, password, email, cargo, equipa) VALUES (@Name, @Pass, @Email, @Cargo, @Team)";

            using (MySqlCommand cmd = new MySqlCommand(createAccountQuery, cn))
            {

                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@Pass", password);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Cargo", cargo);
                cmd.Parameters.AddWithValue("@Team", team);


                // Execute
                cmd.ExecuteNonQuery();

                MessageBox.Show("A sua conta foi criada!", "Sucesso");
            }

            cn.Close();
        }


        //::::::::::::::RESET ID´S::::::::::::::::::
        public static void ResetAutoIncrement(string tabela)
        {
            var cn = ConnectToMySql();
            string createAccountQuery = $"ALTER TABLE {tabela} AUTO_INCREMENT = 1";

            using (MySqlCommand cmd = new MySqlCommand(createAccountQuery, cn))
            {
                cmd.ExecuteNonQuery();
            }

            cn.Close();
        }

        public static class PerfilNameGlobal
        {
            public static string perfilNameToGet;
            public static int perfilNameId;
        }



        //::::::::::::::LOGIN::::::::::::::::::
        public static bool CheckLoginAccount(string nameInput, string passwordInput)
        {
            var cn = ConnectToMySql();
            string selectUsers = "SELECT id, nome, password, cargo FROM usersfoot";

            using (MySqlCommand cmd = new MySqlCommand(selectUsers, cn))
            {
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        PerfilNameGlobal.perfilNameId = reader.GetInt32(reader.GetOrdinal("id"));
                        string nome = reader["nome"].ToString();
                        string password = reader["password"].ToString();
                        PerfilNameGlobal.perfilNameToGet = reader["cargo"].ToString();


                        if (nome == nameInput && password == passwordInput)
                        {
                            cn.Close();
                            return true;
                        }
                    }
                }

                cn.Close();

            }
            return false;
        }



        //::::::::::::::TABLE GET ALL PLAYER::::::::::::::::::

        public static DataTable GetAllPlayers()
        {
            DataTable dataTable = new DataTable();
            var cn = ConnectToMySql();
            string allPlayers = $"SELECT * FROM playersfoot WHERE interessado = 0";

            using (MySqlCommand cmd = new MySqlCommand(allPlayers, cn))
            {
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    dataTable.Load(reader);
                }

                cn.Close();
            }

            return dataTable;
        }

        public static DataTable GetAllInterestedPlayers()
        {
            DataTable dataTable = new DataTable();
            var cn = ConnectToMySql();
            string allPlayers = $"SELECT * FROM playersfoot WHERE interessado = 1";

            using (MySqlCommand cmd = new MySqlCommand(allPlayers, cn))
            {
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    dataTable.Load(reader);
                }

                cn.Close();
            }

            return dataTable;
        }


        //::::::::::::::INSERT PLAYER::::::::::::::::::

        public static void AddPlayer(string name, string position, string age, string nationality, string team, string height, string foot, string shoot, string pass, string defense)
        {
            var cn = ConnectToMySql();

            string createAccountQuery = "INSERT INTO playersfoot (nome, posicao, idade, nacionalidade, equipa, altura, pe_dominante, chute, passe, defesa) VALUES (@Name, @Position, @Age, @Nation, @Team, @Height, @Foot, @Shoot, @Pass, @Defense)";

            using (MySqlCommand cmd = new MySqlCommand(createAccountQuery, cn))
            {

                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@Position", position);
                cmd.Parameters.AddWithValue("@Age", age);
                cmd.Parameters.AddWithValue("@Nation", nationality);
                cmd.Parameters.AddWithValue("@Team", team);
                cmd.Parameters.AddWithValue("@Height", height);
                cmd.Parameters.AddWithValue("@Foot", foot);
                cmd.Parameters.AddWithValue("@Shoot", shoot);
                cmd.Parameters.AddWithValue("@Pass", pass);
                cmd.Parameters.AddWithValue("@Defense", defense);


                // Execute
                cmd.ExecuteNonQuery();

                MessageBox.Show("Jogador inserido com sucesso!", "Sucesso");
            }

            cn.Close();

        }



        //::::::::::::::DELETE PLAYER::::::::::::::::::
        public static void DeletePlayer(string name, string position, string age, string team, string height)
        {
            int id = GetPlayerId(name, age, position, team, height);
            var cn = ConnectToMySql();
            string deletePlayer = "DELETE FROM playersfoot WHERE id = @Id";

            using (MySqlCommand cmd = new MySqlCommand(deletePlayer, cn))
            {
                cmd.Parameters.AddWithValue("@Id", id);

                cmd.ExecuteNonQuery();
            }
            ResetAutoIncrement("playersfoot");
            cn.Close();

        }



        //::::::::::::::INTERESTED PLAYER ON::::::::::::::::::
        public static void AddInterestedPlayer(int playerId)
        {
            var cn = ConnectToMySql();

            string addInterested = "UPDATE playersfoot SET interessado = 1 WHERE id = @PlayerId";

            using (MySqlCommand cmd = new MySqlCommand(addInterested, cn))
            {

                cmd.Parameters.AddWithValue("@PlayerId", playerId);

                // Execute
                cmd.ExecuteNonQuery();

                MessageBox.Show("Jogador na lista de interessados!", "Sucesso");

            }

            cn.Close();

        }


        //::::::::::::::INTERESTED PLAYER OFF::::::::::::::::::
        public static void RemoveInterestedPlayer(int playerId)
        {
            var cn = ConnectToMySql();

            string addInterested = "UPDATE playersfoot SET interessado = 0 WHERE id = @PlayerId";

            using (MySqlCommand cmd = new MySqlCommand(addInterested, cn))
            {

                cmd.Parameters.AddWithValue("@PlayerId", playerId);

                // Execute
                cmd.ExecuteNonQuery();

                

            }

            cn.Close();

        }



        //::::::::::::::GET PLAYER ID::::::::::::::::::
        public static int GetPlayerId(string name, string age, string position, string team, string height)
        {

            var cn = ConnectToMySql();
            int id = 0;
            string allPlayers = $"SELECT id FROM playersfoot WHERE nome = @Name AND posicao = @Position AND idade = @Age AND equipa = @Team AND altura = @Height";

            using (MySqlCommand cmd = new MySqlCommand(allPlayers, cn))
            {
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@Position", position);
                cmd.Parameters.AddWithValue("@Age", age);
                cmd.Parameters.AddWithValue("@Team", team);
                cmd.Parameters.AddWithValue("@Height", height);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        id = reader.GetInt32(reader.GetOrdinal("id"));
                    }

                }

                cn.Close();
            }

            return id;
        }



        //::::::::::::::GET PLAYER NAME::::::::::::::::::
        public static string GetPlayerName(int playerId)
        {
            string name = "";
            var cn = ConnectToMySql();
            string allPlayers = $"SELECT nome FROM playersfoot WHERE id = @Id";

            using (MySqlCommand cmd = new MySqlCommand(allPlayers, cn))
            {
                cmd.Parameters.AddWithValue("@Id", playerId);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        name = reader["nome"].ToString();
                    }

                }

                cn.Close();
            }

            return name;
        }





        //GET TEAM USER
        public static string GetTeamUser(int id)
        {

            var cn = ConnectToMySql();
            string team = "";
            string allPlayers = $"SELECT equipa FROM usersfoot WHERE id = @Id";

            using (MySqlCommand cmd = new MySqlCommand(allPlayers, cn))
            {
                cmd.Parameters.AddWithValue("@Id", id);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        team = reader["equipa"].ToString();
                    }

                }

                cn.Close();
            }

            return team;
        }



        //GET TEAM Player
        public static string GetTeamPlayer(int id)
        {

            var cn = ConnectToMySql();
            string team = "";
            string allPlayers = $"SELECT equipa FROM playersfoot WHERE id = @Id";

            using (MySqlCommand cmd = new MySqlCommand(allPlayers, cn))
            {
                cmd.Parameters.AddWithValue("@Id", id);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        team = reader["equipa"].ToString();
                    }

                }

                cn.Close();
            }

            return team;
        }



        //GET MONEY TEAM
        public static string GetMoneyTeam(string nameClub)
        {

            var cn = ConnectToMySql();
            string money = "";
            string allPlayers = $"SELECT dinheiro FROM clubsfoot WHERE nome = @ClubName";

            using (MySqlCommand cmd = new MySqlCommand(allPlayers, cn))
            {
                cmd.Parameters.AddWithValue("@ClubName", nameClub);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        money = reader["dinheiro"].ToString();

                    }
                }

                cn.Close();
            }

            return money;
        }





        //GET COUNT PLAYERS INTERESTED

        public static int CountInterested()
        {
            var cn = ConnectToMySql();
            int count = 0;

            string addInterested = "SELECT COUNT(*) AS \"playersInteressados\" FROM playersfoot WHERE interessado = 1";

            using (MySqlCommand cmd = new MySqlCommand(addInterested, cn))
            {

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        count = reader.GetInt32(reader.GetOrdinal("playersInteressados"));
                    }

                }

                cn.Close();
            }

            return count;
        }


        //ADD PROPOSAL

        public static void AddProposal(string playerName, string playerTeam, int scoutId, string duration, string importance, string salarie, string interestedTeam)
        {
            var cn = ConnectToMySql();

            string createProposalQuery = "INSERT INTO proposalsfoot (playerName, equipa_do_jogador, scoutid, duração_contrato, importancia, salario, equipa_interessada) VALUES (@PlayerName, @PlayerTeam, @ScoutId, @Duration, @Importance, @Salarie, @TeamInterested)";

            using (MySqlCommand cmd = new MySqlCommand(createProposalQuery, cn))
            {

                cmd.Parameters.AddWithValue("@PlayerName", playerName);
                cmd.Parameters.AddWithValue("@PlayerTeam", playerTeam);
                cmd.Parameters.AddWithValue("@ScoutId", scoutId);
                cmd.Parameters.AddWithValue("@Duration", duration);
                cmd.Parameters.AddWithValue("@Importance", importance);
                cmd.Parameters.AddWithValue("@Salarie", salarie);
                cmd.Parameters.AddWithValue("@TeamInterested", interestedTeam);


                // Execute
                cmd.ExecuteNonQuery();

                MessageBox.Show("Proposta enviada com sucesso!", "Sucesso");
            }

            cn.Close();

        }


        //GET PROPOSALS 

        public static DataTable GetProposals(string coachTeam)
        {
            DataTable dataTable = new DataTable();
            var cn = ConnectToMySql();

            string proposals = "SELECT * FROM proposalsfoot WHERE equipa_do_jogador = @Coach";

            using (MySqlCommand cmd = new MySqlCommand(proposals, cn))
            {
                cmd.Parameters.AddWithValue("@Coach", coachTeam);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    dataTable.Load(reader);
                }

                cn.Close();
            }

            return dataTable;
        }




        //GET COUNT PROPOSALS INTERESTED

        public static int CountProposals(string coachTeam)
        {
            var cn = ConnectToMySql();
            int count = 0;

            string countProposals = "SELECT COUNT(*) AS \"countProposals\" FROM proposalsfoot WHERE equipa_do_jogador = @Coach";

            using (MySqlCommand cmd = new MySqlCommand(countProposals, cn))
            {
                cmd.Parameters.AddWithValue("@Coach", coachTeam);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        count = reader.GetInt32(reader.GetOrdinal("countProposals"));
                    }

                }

                cn.Close();
            }

            return count;
        }



        //DELETE ACCOUNT

        public static void DeleteAccount(int id)
        {
            var cn = ConnectToMySql();
            string deleteUser = "DELETE FROM usersfoot WHERE id = @Id";

            using (MySqlCommand cmd = new MySqlCommand(deleteUser, cn))
            {
                cmd.Parameters.AddWithValue("@Id", id);

                cmd.ExecuteNonQuery();
            }
            ResetAutoIncrement("usersfoot");
            cn.Close();

        }

        public static void DeleteAccountProposals(int id)
        {
            var cn = ConnectToMySql();
            string deleteProposals = "DELETE FROM proposalsfoot WHERE scoutId = @Id";

            using (MySqlCommand cmd = new MySqlCommand(deleteProposals, cn))
            {
                cmd.Parameters.AddWithValue("@Id", id);

                cmd.ExecuteNonQuery();
            }
            ResetAutoIncrement("proposalsfoot");
            cn.Close();

        }

        //DELETE SPECIFIC PROPOSAL
        public static void DeleteSpecificProposal(int proposalId)
        {
            var cn = ConnectToMySql();
            string deleteProposals = "DELETE FROM proposalsfoot WHERE id = @Id";

            using (MySqlCommand cmd = new MySqlCommand(deleteProposals, cn))
            {
                cmd.Parameters.AddWithValue("@Id", proposalId);

                cmd.ExecuteNonQuery();
            }
            ResetAutoIncrement("proposalsfoot");

            cn.Close();

        }

         //ACCEPT PROPOSAL


        public static void AcceptProposal(int proposalId, string clubName, int newMoney, int removableMoney, string interestedClub)
        {
            
            var cn = ConnectToMySql();

            string addMoney = "UPDATE clubsfoot SET dinheiro = @NewMoney WHERE nome = @ClubName";

            using (MySqlCommand cmd = new MySqlCommand(addMoney, cn))
            {
                cmd.Parameters.AddWithValue("@NewMoney", newMoney);
                cmd.Parameters.AddWithValue("@ClubName", clubName);
                

                // Execute
                cmd.ExecuteNonQuery();


            }
            RemoveMoney(proposalId, interestedClub, removableMoney);
            cn.Close();

        }

        public static void RemoveMoney(int proposalId, string clubName, int newMoney)
        {
            
            var cn = ConnectToMySql();

            string removeMoney = "UPDATE clubsfoot SET dinheiro = @NewMoney WHERE nome = @ClubName";

            using (MySqlCommand cmd = new MySqlCommand(removeMoney, cn))
            {

                cmd.Parameters.AddWithValue("@NewMoney", newMoney);
                cmd.Parameters.AddWithValue("@ClubName", clubName);

                // Execute
                cmd.ExecuteNonQuery();


            }
            
            cn.Close();

        }


        //GET PLAYER BY PROPOSAL
        public static string GetPlayerByProposal(int proposalId)
        {

            var cn = ConnectToMySql();
            string player = "";
            string allPlayers = $"SELECT playerName FROM proposalsfoot WHERE id = @ProposalId";

            using (MySqlCommand cmd = new MySqlCommand(allPlayers, cn))
            {
                cmd.Parameters.AddWithValue("@ProposalId", proposalId);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        player = reader["playerName"].ToString();

                    }
                }

                cn.Close();
            }

            return player;
        }



        //::::::::::::::GET PLAYER ID ONLY NAME::::::::::::::::::
        public static int GetPlayerIdSimple(string name)
        {

            var cn = ConnectToMySql();
            int id = 0;
            string allPlayers = $"SELECT id FROM playersfoot WHERE nome = @Name";

            using (MySqlCommand cmd = new MySqlCommand(allPlayers, cn))
            {
                cmd.Parameters.AddWithValue("@Name", name);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        id = reader.GetInt32(reader.GetOrdinal("id"));
                    }

                }

                cn.Close();
            }

            return id;
        }



        //UPDATE PLAYER TEAM
        public static void UpdateTeam(int playerid, string newTeam)
        {
            
            var cn = ConnectToMySql();

            string removeMoney = "UPDATE playersfoot SET equipa = @NewTeam WHERE id = @Id";

            using (MySqlCommand cmd = new MySqlCommand(removeMoney, cn))
            {

                cmd.Parameters.AddWithValue("@NewTeam", newTeam);
                cmd.Parameters.AddWithValue("@Id", playerid);

                // Execute
                cmd.ExecuteNonQuery();


            }
            
            cn.Close();

        }



    }
}
