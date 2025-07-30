using KutuphaneYonetimSistemiGUI.Managers;
using KutuphaneYonetimSistemiGUI.Models;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using KutuphaneYonetimSistemiGUI.SQLManeger;
namespace KutuphaneYonetimSistemiGUI.Forms
{
    public partial class AdminForm : Form
    {
        private readonly BookManager _bookManager;
        private readonly LoanManager _loanManager;
        private readonly SqlBookManager _sqlBookManager;
        private DataGridView dgvBooks;
        private Button btnAdd, btnDelete, btnUpdate, btnRefresh, btnLoan;
        private TextBox txtTitle, txtAuthor, txtISBN;
        private Label lblTitle, lblAuthor, lblISBN, lblHeader, lblLoanInfo;
        private ComboBox cmbUsers;
        private DateTimePicker dtpLoanDate;
        private DataGridView dgvLoanRequests;
        private Button btnApproveSelectedRequest;


        public AdminForm()
        {
            _bookManager = new BookManager();
            _loanManager = new LoanManager();
            InitializeComponent();
            LoadBooks();
            LoadPendingLoanRequests();

        }

        private void InitializeComponent()
        {
            this.Text = "Admin Paneli - Kitap Yönetimi";
            this.ClientSize = new Size(900, 600);
            this.BackColor = Color.White;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Font = new Font("Segoe UI", 10, FontStyle.Regular);

            lblHeader = new Label
            {
                Text = "Kütüphane Yönetim Sistemi - Admin Paneli",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = Color.DarkSlateBlue,
                AutoSize = true,
                Location = new Point(20, 15)
            };
            this.Controls.Add(lblHeader);

            dgvBooks = new DataGridView
            {
                Location = new Point(20, 60),
                Size = new Size(550, 500),
                ReadOnly = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                EnableHeadersVisualStyles = false,
                ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
                {
                    BackColor = Color.DarkSlateBlue,
                    ForeColor = Color.White,
                    Font = new Font("Segoe UI", 10, FontStyle.Bold)
                },
                RowHeadersVisible = false,
                AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle
                {
                    BackColor = Color.AliceBlue
                }
            };
            this.Controls.Add(dgvBooks);

            Panel controlPanel = new Panel
            {
                Location = new Point(590, 60),
                Size = new Size(290, 500),
                BackColor = Color.WhiteSmoke,
                BorderStyle = BorderStyle.FixedSingle
            };
            this.Controls.Add(controlPanel);

            lblTitle = new Label { Text = "Kitap Başlığı:", Location = new Point(15, 30), AutoSize = true };
            txtTitle = new TextBox { Location = new Point(15, 60), Width = 260, BorderStyle = BorderStyle.FixedSingle };

            lblAuthor = new Label { Text = "Yazar:", Location = new Point(15, 100), AutoSize = true };
            txtAuthor = new TextBox { Location = new Point(15, 130), Width = 260, BorderStyle = BorderStyle.FixedSingle };

            lblISBN = new Label { Text = "ISBN:", Location = new Point(15, 170), AutoSize = true };
            txtISBN = new TextBox { Location = new Point(15, 200), Width = 260, BorderStyle = BorderStyle.FixedSingle };

            btnAdd = new Button
            {
                Text = "Kitap Ekle",
                Location = new Point(15, 250),
                Size = new Size(125, 40),
                BackColor = Color.SeaGreen,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnAdd.FlatAppearance.BorderSize = 0;
            btnAdd.Click += BtnAdd_Click;

            btnUpdate = new Button
            {
                Text = "Güncelle",
                Location = new Point(150, 250),
                Size = new Size(125, 40),
                BackColor = Color.DodgerBlue,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnUpdate.FlatAppearance.BorderSize = 0;
            btnUpdate.Click += BtnUpdate_Click;

            btnDelete = new Button
            {
                Text = "Kitap Sil",
                Location = new Point(15, 310),
                Size = new Size(260, 40),
                BackColor = Color.IndianRed,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnDelete.FlatAppearance.BorderSize = 0;
            btnDelete.Click += BtnDelete_Click;

            btnRefresh = new Button
            {
                Text = "Listeyi Yenile",
                Location = new Point(15, 370),
                Size = new Size(260, 40),
                BackColor = Color.DarkSlateBlue,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnRefresh.FlatAppearance.BorderSize = 0;
            btnRefresh.Click += BtnRefresh_Click;

            cmbUsers = new ComboBox
            {
                Location = new Point(15, 430),
                Width = 260,
                DropDownStyle = ComboBoxStyle.DropDownList,
                Visible = false
            };
            cmbUsers.Items.AddRange(new string[] { "ahmet", "ayşe", "mehmet" });

            dtpLoanDate = new DateTimePicker
            {
                Location = new Point(15, 460),
                Width = 260,
                Format = DateTimePickerFormat.Short,
                Value = DateTime.Today,
                Visible = false
            };

            btnLoan = new Button
            {
                Text = "Ödünç Ver",
                Location = new Point(15, 490),
                Size = new Size(260, 40),
                BackColor = Color.MediumPurple,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                Visible = false
            };
            btnLoan.FlatAppearance.BorderSize = 0;
            btnLoan.Click += BtnLoan_Click;

            lblLoanInfo = new Label
            {
                Location = new Point(15, 430),
                AutoSize = true,
                ForeColor = Color.Maroon,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                Visible = false
            };

            controlPanel.Controls.Add(lblTitle);
            controlPanel.Controls.Add(txtTitle);
            controlPanel.Controls.Add(lblAuthor);
            controlPanel.Controls.Add(txtAuthor);
            controlPanel.Controls.Add(lblISBN);
            controlPanel.Controls.Add(txtISBN);
            controlPanel.Controls.Add(btnAdd);
            controlPanel.Controls.Add(btnUpdate);
            controlPanel.Controls.Add(btnDelete);
            controlPanel.Controls.Add(btnRefresh);
            controlPanel.Controls.Add(cmbUsers);
            controlPanel.Controls.Add(dtpLoanDate);
            controlPanel.Controls.Add(btnLoan);
            controlPanel.Controls.Add(lblLoanInfo);
            // Bekleyen istekleri gösteren DataGridView
            dgvLoanRequests = new DataGridView
            {
                Location = new Point(20, 370), 
                Size = new Size(550, 150),
                ReadOnly = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                BackgroundColor = Color.White,
                Visible = true
            };
            this.Controls.Add(dgvLoanRequests);

            // Onayla butonu
            btnApproveSelectedRequest = new Button
            {
                Text = "Seçili İsteği Onayla",
                Location = new Point(590, 570),
                Size = new Size(290, 40),
                BackColor = Color.MediumSeaGreen,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnApproveSelectedRequest.FlatAppearance.BorderSize = 0;
            btnApproveSelectedRequest.Click += BtnApproveSelectedRequest_Click;
            this.Controls.Add(btnApproveSelectedRequest);


            dgvBooks.SelectionChanged += (sender, e) =>
            {
                if (dgvBooks.SelectedRows.Count > 0)
                {
                    var selectedBook = dgvBooks.SelectedRows[0].DataBoundItem as Book;
                    if (selectedBook != null)
                    {
                        txtTitle.Text = selectedBook.Title;
                        txtAuthor.Text = selectedBook.Author;
                        txtISBN.Text = selectedBook.ISBN;

                        if (selectedBook.IsAvailable)
                        {
                            cmbUsers.Visible = true;
                            dtpLoanDate.Visible = true;
                            btnLoan.Visible = true;
                            lblLoanInfo.Visible = false;
                        }
                        else
                        {
                            cmbUsers.Visible = false;
                            dtpLoanDate.Visible = false;
                            btnLoan.Visible = false;
                            lblLoanInfo.Text = $"Bu kitap {selectedBook.BorrowedBy} tarafından ödünç alınmış.";
                            lblLoanInfo.Visible = true;
                        }
                    }
                }
            };
        }
        private void LoadPendingLoanRequests()
        {
            var pendingRequests = _loanManager.GetPendingRequests();
            dgvLoanRequests.DataSource = null;
            dgvLoanRequests.DataSource = pendingRequests;

            if (pendingRequests.Any())
            {
                dgvLoanRequests.Columns["Username"].HeaderText = "Kullanıcı";
                dgvLoanRequests.Columns["ISBN"].HeaderText = "ISBN";
                dgvLoanRequests.Columns["RequestDate"].HeaderText = "Talep Tarihi";
                dgvLoanRequests.Columns["Status"].HeaderText = "Durum";
            }
        }
        private void BtnApproveSelectedRequest_Click(object sender, EventArgs e)
        {
            if (dgvLoanRequests.SelectedRows.Count == 0)
            {
                MessageBox.Show("Lütfen bir istek seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedRequest = dgvLoanRequests.SelectedRows[0].DataBoundItem as LoanRequest;
            if (selectedRequest != null)
            {
                _loanManager.ApproveRequest(selectedRequest);
                MessageBox.Show("İstek başarıyla onaylandı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadBooks(); // kitap durumu da güncellenir
                LoadPendingLoanRequests(); // yeniden yükle
            }
        }


        private void LoadBooks()
        {
            try
            {
                dgvBooks.DataSource = null;
                var books = _bookManager.GetAllBooks();
                dgvBooks.DataSource = books;

                if (books.Any())
                {
                    dgvBooks.Columns["Title"].HeaderText = "Kitap Adı";
                    dgvBooks.Columns["Author"].HeaderText = "Yazar";
                    dgvBooks.Columns["ISBN"].HeaderText = "ISBN Numarası";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Kitaplar yüklenirken bir hata oluştu: {ex.Message}", "Hata",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtTitle.Text) ||
                    string.IsNullOrWhiteSpace(txtAuthor.Text) ||
                    string.IsNullOrWhiteSpace(txtISBN.Text))
                {
                    MessageBox.Show("Lütfen tüm alanları doldurunuz.", "Uyarı",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                Book newBook = new Book
                {
                    Title = txtTitle.Text.Trim(),
                    Author = txtAuthor.Text.Trim(),
                    ISBN = txtISBN.Text.Trim(),
                    PublishYear = 0
                };

                _bookManager.AddBook(newBook);
                _sqlBookManager.AddBook(newBook);

                LoadBooks();
                ClearFields();

                MessageBox.Show("Kitap başarıyla eklendi.", "Başarılı",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}", "Hata",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtISBN.Text))
                {
                    MessageBox.Show("Lütfen güncellenecek bir kitap seçiniz.", "Uyarı",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                Book updatedBook = new Book
                {
                    Title = txtTitle.Text.Trim(),
                    Author = txtAuthor.Text.Trim(),
                    ISBN = txtISBN.Text.Trim()
                };

                _bookManager.UpdateBook(updatedBook);
                _sqlBookManager.UpdateBook(updatedBook);
                LoadBooks();

                MessageBox.Show("Kitap başarıyla güncellendi.", "Başarılı",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}", "Hata",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtISBN.Text))
                {
                    MessageBox.Show("Lütfen silinecek bir kitap seçiniz.", "Uyarı",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var result = MessageBox.Show("Bu kitabı silmek istediğinize emin misiniz?", "Onay",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    string isbn = txtISBN.Text.Trim();
                    _bookManager.DeleteBook(isbn);
                    _sqlBookManager.DeleteBook(isbn);
                    LoadBooks();
                    ClearFields();

                    MessageBox.Show("Kitap başarıyla silindi.", "Başarılı",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}", "Hata",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            LoadBooks();
            ClearFields();
        }

        private void BtnLoan_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbUsers.SelectedItem == null)
                {
                    MessageBox.Show("Lütfen bir kullanıcı seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string username = cmbUsers.SelectedItem.ToString();
                string isbn = txtISBN.Text.Trim();

                _loanManager.LoanBook(username, isbn);
                LoadBooks();
                ClearFields();

                MessageBox.Show("Kitap başarıyla ödünç verildi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearFields()
        {
            txtTitle.Clear();
            txtAuthor.Clear();
            txtISBN.Clear();
            cmbUsers.SelectedItem = null;
            dtpLoanDate.Value = DateTime.Today;
        }
    }
}
