using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Products_DRUD
{
    public partial class Form1 : Form
    {
        private Connection db;

        public Form1()
        {
            InitializeComponent();
            db = Connection.GetInstance();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            if (InputsValid())
            {
                string query = "INSERT INTO pizzas(nome, descricao, preco) VALUES(@name, @description, @price)";
                MySqlParameter[] parameters = {
                new MySqlParameter("@name", txtNome.Text),
                new MySqlParameter("@description", txtDescricao.Text),
                new MySqlParameter("@price", txtPreco.Text)
            };

                db.ExecuteNonQuery(query, parameters);
                MessageBox.Show("Pizza criada com sucesso!");
                btn_LoadTable(sender, e);
            } else
            {
                MessageBox.Show("Insira valores válidos nos Inputs");
            }
            
        }

        private void btn_LoadTable(object sender, EventArgs e)
        {
            string query = "SELECT * FROM pizzas";
            string query2 = "SELECT * FROM clientes";
            string query3 = "SELECT * FROM pedidos";
            DataTable dt = db.ExecuteQuery(query);
            DataTable dt2 = db.ExecuteQuery(query2);
            DataTable dt3 = db.ExecuteQuery(query3);
            dataGrid.DataSource = dt;
            dataGrid2.DataSource = dt2;
            dataGrid3.DataSource = dt3;
        }

        private void FillFormFromGrid(DataGridView dg, TextBox name, TextBox description, TextBox price)
        {
            if (dg.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dg.SelectedRows[0];

                name.Text = selectedRow.Cells["nome"].Value.ToString();
                description.Text = selectedRow.Cells["descricao"].Value.ToString();
                price.Text = selectedRow.Cells["preco"].Value.ToString();
            }
        }

        private bool InputsValid()
        {
            if (string.IsNullOrWhiteSpace(txtNome.Text) ||
                string.IsNullOrWhiteSpace(txtDescricao.Text) ||
                string.IsNullOrWhiteSpace(txtPreco.Text))
            {
                return false;
            }

            return true;
        }


        private void grid_SelectionChange(object sender, EventArgs e)
        {
            FillFormFromGrid(dataGrid, txtNome, txtDescricao, txtPreco);
        }

        private int ShowPopupForProductSelection(int AltDel)
        {
            Form popup = new Form();
            ComboBox cbSelectProduct = new ComboBox();
            Button btnSelectProduct = new Button();
            Label lblSelectProduct = new Label();
            int selectedProductId = -1;

            cbSelectProduct.DropDownStyle = ComboBoxStyle.DropDownList;
            cbSelectProduct.DataSource = GetProducts();  
            cbSelectProduct.DisplayMember = "nome"; 
            cbSelectProduct.ValueMember = "id_pizza";

            lblSelectProduct.Text = "Selecione a pizza:";
            lblSelectProduct.AutoSize = true;

            if (AltDel == 0)
                btnSelectProduct.Text = "Alterar";
            else
                btnSelectProduct.Text = "Deletar";
            btnSelectProduct.DialogResult = DialogResult.OK;
            btnSelectProduct.Size = new Size(100, 30);

            Panel panel = new Panel();
            panel.Size = new Size(300, 150);
            panel.Dock = DockStyle.Fill;

            lblSelectProduct.Location = new Point(10, 10);
            cbSelectProduct.Location = new Point(10, 40);
            cbSelectProduct.Size = new Size(260, 25);
            btnSelectProduct.Location = new Point(100, 80);  

            panel.Controls.Add(lblSelectProduct);
            panel.Controls.Add(cbSelectProduct);
            panel.Controls.Add(btnSelectProduct);

            popup.ClientSize = new Size(320, 150);
            popup.Controls.Add(panel);
            popup.Text = "Selecionar Produto";

            if (popup.ShowDialog() == DialogResult.OK)
            {
                if (cbSelectProduct.SelectedItem != null)
                {
                    var selectedProduct = (DataRowView)cbSelectProduct.SelectedItem;
                    selectedProductId = (int)selectedProduct["id_pizza"];
                }
            }

            return selectedProductId;
        }


        private DataTable GetProducts()
        {
            string query = "SELECT id_pizza, nome FROM pizzas";
            return db.ExecuteQuery(query);
        }


        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (InputsValid())
            {

                int selectedProductId = ShowPopupForProductSelection(0);

                if (selectedProductId != -1)  
                {
                    string query = "UPDATE pizzas SET nome = @name, descricao = @description, preco = @price WHERE id_pizza = @id";

                    MySqlParameter[] parameters = {
                        new MySqlParameter("@name", txtNome.Text),
                        new MySqlParameter("@description", txtDescricao.Text),
                        new MySqlParameter("@price", txtPreco.Text),
                        new MySqlParameter("@id", selectedProductId)  
                    };

                    db.ExecuteNonQuery(query, parameters);
                    MessageBox.Show("Pizza atualizada com sucesso!");

                    btn_LoadTable(sender, e);  
                }
                else
                {
                    MessageBox.Show("Nenhuma pizza selecionada!");
                }
            } else
            {
                MessageBox.Show("Insira valores válidos nos Inputs");
            }
        }


        private void btnDelete_Click(object sender, EventArgs e)
        {
            int selectedProductId = ShowPopupForProductSelection(1);

            if (selectedProductId != -1)  
            {
                string query = "DELETE from pizzas WHERE id_pizza = @id";

                MySqlParameter[] parameters = {
                    new MySqlParameter("@id", selectedProductId)  
                };

                db.ExecuteNonQuery(query, parameters);
                MessageBox.Show("Pizza deletada com sucesso!");

                btn_LoadTable(sender, e);  
            }
            else
            {
                MessageBox.Show("Nenhuma pizza selecionada!");
            }
        }

        private bool Inputs2Valid()
        {
            if (string.IsNullOrWhiteSpace(txtClienteNome.Text) ||
                string.IsNullOrWhiteSpace(txtTelefone.Text) ||
                string.IsNullOrWhiteSpace(txtEndereco.Text))
            {
                return false; 
            }

            return true; 
        }

        private void btnCreate2_Click(object sender, EventArgs e)
        {

            if (Inputs2Valid())
            {
                string query = "INSERT INTO clientes(nome, telefone, endereco) VALUES(@name, @telephone, @address)";
                MySqlParameter[] parameters = {
                new MySqlParameter("@name", txtClienteNome.Text),
                new MySqlParameter("@telephone", txtTelefone.Text),
                new MySqlParameter("@address", txtEndereco.Text)
            };

                db.ExecuteNonQuery(query, parameters);
                MessageBox.Show("Cliente criado com sucesso!");
                btn_LoadTable(sender, e);
            }
            else
            {
                MessageBox.Show("Insira valores válidos nos Inputs");
            }

        }

        private void btnUpdate2_Click(object sender, EventArgs e)
        {

            if (Inputs2Valid())
            {

                int selectedClientId = ShowPopupForClientSelection(0);

                if (selectedClientId != -1)  
                {
                    string query = "UPDATE clientes SET nome = @name, telefone = @telephone, endereco = @address WHERE id_cliente = @id";

                    MySqlParameter[] parameters = {
                        new MySqlParameter("@name", txtClienteNome.Text),
                        new MySqlParameter("@telephone", txtTelefone.Text),
                        new MySqlParameter("@address", txtEndereco.Text),
                        new MySqlParameter("@id", selectedClientId)  
                    };

                    db.ExecuteNonQuery(query, parameters);
                    MessageBox.Show("Cliente atualizado com sucesso!");

                    btn_LoadTable(sender, e);  
                }
                else
                {
                    MessageBox.Show("Nenhum cliente selecionado!");
                }
            }
            else
            {
                MessageBox.Show("Insira valores válidos nos Inputs");
            }
        }

        private int ShowPopupForClientSelection(int AltDel)
        {
            Form popup = new Form();
            ComboBox cbSelectClient = new ComboBox();
            Button btnSelectClient = new Button();
            Label lblSelectClient = new Label();
            int selectedClientId = -1;  

            cbSelectClient.DropDownStyle = ComboBoxStyle.DropDownList;
            cbSelectClient.DataSource = GetClients();  
            cbSelectClient.DisplayMember = "nome"; 
            cbSelectClient.ValueMember = "id_cliente"; 

            lblSelectClient.Text = "Selecione o cliente:";
            lblSelectClient.AutoSize = true;

            if (AltDel == 0)
                btnSelectClient.Text = "Alterar";
            else
                btnSelectClient.Text = "Deletar";
            btnSelectClient.DialogResult = DialogResult.OK;
            btnSelectClient.Size = new Size(100, 30);

            Panel panel = new Panel();
            panel.Size = new Size(300, 150);
            panel.Dock = DockStyle.Fill;

            lblSelectClient.Location = new Point(10, 10);
            cbSelectClient.Location = new Point(10, 40);
            cbSelectClient.Size = new Size(260, 25);
            btnSelectClient.Location = new Point(100, 80);  

            panel.Controls.Add(lblSelectClient);
            panel.Controls.Add(cbSelectClient);
            panel.Controls.Add(btnSelectClient);

            popup.ClientSize = new Size(320, 150);
            popup.Controls.Add(panel);
            popup.Text = "Selecionar Cliente";

            if (popup.ShowDialog() == DialogResult.OK)
            {
                if (cbSelectClient.SelectedItem != null)
                {
                    var selectedProduct = (DataRowView)cbSelectClient.SelectedItem;
                    selectedClientId = (int)selectedProduct["id_cliente"];
                }
            }

            return selectedClientId;
        }

        private DataTable GetClients()
        {
            string query = "SELECT id_cliente, nome FROM clientes";
            return db.ExecuteQuery(query);
        }

        private void btnDelete2_Click(object sender, EventArgs e)
        {

            int selectedClientId = ShowPopupForClientSelection(1);

            if (selectedClientId != -1)  
            {
                string query = "DELETE from clientes WHERE id_cliente = @id";

                MySqlParameter[] parameters = {
                    new MySqlParameter("@id", selectedClientId)  
                };

                db.ExecuteNonQuery(query, parameters);
                MessageBox.Show("Cliente deletado com sucesso!");

                btn_LoadTable(sender, e);  
            }
            else
            {
                MessageBox.Show("Nenhum cliente selecionado!");
            }
        }

        private DataTable GetOrders()
        {
            string query = "SELECT id_pedido, id_cliente FROM pedidos";
            return db.ExecuteQuery(query);
        }

        private int[] ShowPopupForOrderCreation()
        {
            Form popup = new Form();
            ComboBox cbSelectClient = new ComboBox();
            ComboBox cbSelectProduct = new ComboBox();
            Button btnConfirmSelection = new Button();
            Label lblSelectClient = new Label();
            Label lblSelectProduct = new Label();
            int[] selectedIds = { -1, -1 }; 

            cbSelectClient.DropDownStyle = ComboBoxStyle.DropDownList;
            cbSelectClient.DataSource = GetClients(); 
            cbSelectClient.DisplayMember = "nome"; 
            cbSelectClient.ValueMember = "id_cliente"; 

            cbSelectProduct.DropDownStyle = ComboBoxStyle.DropDownList;
            cbSelectProduct.DataSource = GetProducts(); 
            cbSelectProduct.DisplayMember = "nome"; 
            cbSelectProduct.ValueMember = "id_produto"; 

            lblSelectClient.Text = "Selecione o cliente:";
            lblSelectClient.AutoSize = true;

            lblSelectProduct.Text = "Selecione o produto:";
            lblSelectProduct.AutoSize = true;

            btnConfirmSelection.Text = "Confirmar";
            btnConfirmSelection.DialogResult = DialogResult.OK;
            btnConfirmSelection.Size = new Size(100, 30);

            Panel panel = new Panel();
            panel.Size = new Size(300, 200);
            panel.Dock = DockStyle.Fill;

            lblSelectClient.Location = new Point(10, 10);
            cbSelectClient.Location = new Point(10, 40);
            cbSelectClient.Size = new Size(260, 25);

            lblSelectProduct.Location = new Point(10, 80);
            cbSelectProduct.Location = new Point(10, 110);
            cbSelectProduct.Size = new Size(260, 25);

            btnConfirmSelection.Location = new Point(100, 150); 

            panel.Controls.Add(lblSelectClient);
            panel.Controls.Add(cbSelectClient);
            panel.Controls.Add(lblSelectProduct);
            panel.Controls.Add(cbSelectProduct);
            panel.Controls.Add(btnConfirmSelection);

            popup.ClientSize = new Size(320, 230);
            popup.Controls.Add(panel);
            popup.Text = "Selecionar Cliente e Produto";

            if (popup.ShowDialog() == DialogResult.OK)
            {
                if (cbSelectClient.SelectedItem != null && cbSelectProduct.SelectedItem != null)
                {
                    var selectedClient = (DataRowView)cbSelectClient.SelectedItem;
                    var selectedProduct = (DataRowView)cbSelectProduct.SelectedItem;

                    selectedIds[0] = (int)selectedClient["id_cliente"];
                    selectedIds[1] = (int)selectedProduct["id_pizza"];
                }
            }

            return selectedIds;
        }

        private int ShowPopupForOrderDeletion()
        {
            Form popup = new Form
            {
                ClientSize = new Size(350, 200),
                Text = "Selecionar Pedido",
                StartPosition = FormStartPosition.CenterParent 
            };

            Label lblSelectOrder = new Label
            {
                Text = "Selecione o pedido:",
                AutoSize = true,
                Location = new Point(10, 20)
            };

            ComboBox cbSelectOrder = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Location = new Point(10, 50),
                Size = new Size(320, 25)
            };

            Button btnSelectOrder = new Button
            {
                Text = "Deletar",
                DialogResult = DialogResult.OK,
                Location = new Point(120, 100),
                Size = new Size(100, 30)
            };

            cbSelectOrder.DataSource = GetOrders(); 
            cbSelectOrder.DisplayMember = "id_pedido"; 
            cbSelectOrder.ValueMember = "id_pedido"; 

            popup.Controls.Add(lblSelectOrder);
            popup.Controls.Add(cbSelectOrder);
            popup.Controls.Add(btnSelectOrder);

            int selectedOrderId = -1;

            if (popup.ShowDialog() == DialogResult.OK)
            {
                if (cbSelectOrder.SelectedItem != null)
                {
                    var selectedOrder = (DataRowView)cbSelectOrder.SelectedItem;
                    selectedOrderId = (int)selectedOrder["id_pedido"];
                }
            }

            return selectedOrderId;
        }



        private void btnCreate3_Click(object sender, EventArgs e)
        {
            int[] ids = ShowPopupForOrderCreation();

            if (ids[0] != -1 && ids[1] != -1)
            {
            DateTime now = DateTime.Now;

            string query = "INSERT INTO pedidos(data_pedido, id_cliente, id_pizza) VALUES(@date, @idCliente, @idPizza)";
            MySqlParameter[] parameters = {
                new MySqlParameter("@date", now),
                new MySqlParameter("@idCliente", ids[0]),
                new MySqlParameter("@idPizza", ids[1])
            };
            db.ExecuteNonQuery(query, parameters);
            MessageBox.Show("Pedido criado com sucesso!");
            btn_LoadTable(sender, e);
            } else
            {
                MessageBox.Show("Escolha um cliente e uma pizza");
            }
        }

        private void btnDelete3_Click(object sender, EventArgs e)
        {


            int selectedOrderId = ShowPopupForOrderDeletion();

            if (selectedOrderId != -1)  
            {
                string query = "DELETE from pedidos WHERE id_pedido = @id";

                MySqlParameter[] parameters = {
                    new MySqlParameter("@id", selectedOrderId)  
                };

                db.ExecuteNonQuery(query, parameters);
                MessageBox.Show("Pedido deletado com sucesso!");

                btn_LoadTable(sender, e);  
            }
            else
            {
                MessageBox.Show("Nenhum pedido selecionado!");
            }
        }
    }

}
