using DatabaseLogic;
using Npgsql;
using System;
using System.Data;
using System.Windows;


namespace Polyclinic
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public DB _dataBase;

        public MainWindow()
        {
            InitializeComponent();

            _dataBase = new DB();

            DataTable dt = _dataBase.UpdateShow("select surename from polyclinic.patients");

            foreach (DataRow dr in dt.Rows)
            {
                ComboBox_SelectionPatient.Items.Add(dr[0]);
                ComboBox_Patient.Items.Add(dr[0]);
            }

            dt = _dataBase.UpdateShow("select surename from polyclinic.doctors");

            foreach (DataRow dr in dt.Rows)
            {
                ComboBox_Doctor.Items.Add(dr[0]);
            }
        }

        private void Button_Reception_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Note_Click(object sender, RoutedEventArgs e)
        {
            TabControl_All.SelectedItem = TabItem_Note;
        }

        private void Button_Doctors_Click(object sender, RoutedEventArgs e)
        {
            TabControl_All.SelectedItem = TabItem_Doctors;
            DataTable dt = _dataBase.UpdateShow("select id_doctor, name, surename, patronymic, name_specialization, phone_number, name_room from polyclinic.doctors inner join polyclinic.specializations on (polyclinic.specializations.id = polyclinic.doctors.id_specialization) inner join polyclinic.rooms using(room_number)");
            // отображаем на экране
            DataGrid_Doctors.ItemsSource = dt.DefaultView;
            DataGrid_Doctors.AutoGenerateColumns = true;
            DataGrid_Doctors.CanUserAddRows = false;
        }

        private void Button_Patients_Click(object sender, RoutedEventArgs e)
        {
            TabControl_All.SelectedItem = TabItem_Patients;
        }

        private void Button_Schedule_Click(object sender, RoutedEventArgs e)
        {
            TabControl_All.SelectedItem = TabItem_Schedule;
        }

        private void Button_MedCard_Click(object sender, RoutedEventArgs e)
        {
            TabControl_All.SelectedItem = TabItem_MedCard;
            DataTable dt = _dataBase.UpdateShow("");
            // отображаем на экране
            DataGrid_MedCard.ItemsSource = dt.DefaultView;
            DataGrid_MedCard.AutoGenerateColumns = true;
            DataGrid_MedCard.CanUserAddRows = false;
        }
        private void Button_Medicaments_Click(object sender, RoutedEventArgs e)
        {
            if (ComboBox_SelectionPatient.SelectedIndex == -1)
            {
                return;
            }

            int index = ComboBox_SelectionPatient.SelectedIndex;

            DataTable dt = _dataBase.UpdateShow("select id_patient from polyclinic.patients");

            DataRow col = dt.Rows[index];

            int id = Convert.ToInt32(col[0]);

            TabControl_All.SelectedItem = TabItem_Medicaments;

            dt = _dataBase.GetMedicaments(id);

            // отображаем на экране
            DataGrid_Medicaments.ItemsSource = dt.DefaultView;
            DataGrid_Medicaments.AutoGenerateColumns = true;
            DataGrid_Medicaments.CanUserAddRows = false;
        }
        private void Button_Procedures_Click(object sender, RoutedEventArgs e)
        {
            if (ComboBox_SelectionPatient.SelectedIndex == -1)
            {
                return;
            }

            int index = ComboBox_SelectionPatient.SelectedIndex;

            DataTable dt = _dataBase.UpdateShow("select id_patient from polyclinic.patients");

            DataRow col = dt.Rows[index];

            int id = Convert.ToInt32(col[0]);

            TabControl_All.SelectedItem = TabItem_Procedures;

            dt = _dataBase.GetProcedures(id);

            // отображаем на экране
            DataGrid_Procedures.ItemsSource = dt.DefaultView;
            DataGrid_Procedures.AutoGenerateColumns = true;
            DataGrid_Procedures.CanUserAddRows = false;
        }
        private void Button_Diagnoses_Click(object sender, RoutedEventArgs e)
        {
            if (ComboBox_SelectionPatient.SelectedIndex == -1)
            {
                return;
            }

            int index = ComboBox_SelectionPatient.SelectedIndex;

            DataTable dt = _dataBase.UpdateShow("select id_patient from polyclinic.patients");

            DataRow col = dt.Rows[index];

            int id = Convert.ToInt32(col[0]);

            TabControl_All.SelectedItem = TabItem_Diagnoses;

            dt = _dataBase.GetDiagnoses(id);

            // отображаем на экране
            DataGrid_Diagnoses.ItemsSource = dt.DefaultView;
            DataGrid_Diagnoses.AutoGenerateColumns = true;
            DataGrid_Diagnoses.CanUserAddRows = false;
        }
        private void Button_Statistics_Click(object sender, RoutedEventArgs e)
        {
            if (ComboBox_SelectionPatient.SelectedIndex == -1)
            {
                return;
            }

            int index = ComboBox_SelectionPatient.SelectedIndex;

            DataTable dt = _dataBase.UpdateShow("select id_patient from polyclinic.patients");

            DataRow col = dt.Rows[index];

            int id = Convert.ToInt32(col[0]);

            TabControl_All.SelectedItem = TabItem_Statistics;

            dt = _dataBase.GetStatistics(id);

            // отображаем на экране
            DataGrid_Statistics.ItemsSource = dt.DefaultView;
            DataGrid_Statistics.AutoGenerateColumns = true;
            DataGrid_Statistics.CanUserAddRows = false;
        }
        private void Button_StatisticsAll_Click(object sender, RoutedEventArgs e)
        {
            TabControl_All.SelectedItem = TabItem_StatisticsAll;
            DataTable dt = _dataBase.UpdateShow("select name_diagnose, count(id_patient) from polyclinic.medical_cards inner join polyclinic.admission_results using (id_medical_card) inner join polyclinic.diagnoses using (id_diagnose) group by name_diagnose");
            // отображаем на экране
            DataGrid_StatisticsAll.ItemsSource = dt.DefaultView;
            DataGrid_StatisticsAll.AutoGenerateColumns = true;
            DataGrid_StatisticsAll.CanUserAddRows = false;
        }

        private void Button_PatientInfo_Click(object sender, RoutedEventArgs e)
        {
            if (ComboBox_SelectionPatient.SelectedIndex == -1)
            {
                return;
            }

            int index = ComboBox_SelectionPatient.SelectedIndex;

            DataTable dt = _dataBase.UpdateShow("select id_patient from polyclinic.patients");

            DataRow col = dt.Rows[index];

            int id = Convert.ToInt32(col[0]);

            TabControl_All.SelectedItem = TabItem_PatientInfo;

            dt = _dataBase.GetPatientInfo(id);

            // отображаем на экране
            DataGrid_PatientInfo.ItemsSource = dt.DefaultView;
            DataGrid_PatientInfo.AutoGenerateColumns = true;
            DataGrid_PatientInfo.CanUserAddRows = false;
        }

        private void Button_AddPatient_Click(object sender, RoutedEventArgs e)
        {
            TabControl_All.SelectedItem = TabItem_AddPatient;
        }

        private void Button_AddPatientInBD_Click(object sender, RoutedEventArgs e)
        {
            long phone = Convert.ToInt64(TextBox_Phone.Text);
            long policy = Convert.ToInt64(TextBox_Policy.Text);

            _dataBase.AddPatient
                (
                TextBox_Name.Text,
                TextBox_Surename.Text,
                TextBox_Patronymic.Text,
                TextBox_Address.Text,
                phone,
                TextBox_BirthDate.Text,
                policy
                );
            TabControl_All.SelectedItem = TabItem_Patients;
            TextBox_Name.Text = "";
            TextBox_Surename.Text = "";
            TextBox_Patronymic.Text = "";
            TextBox_Address.Text = "";
            TextBox_Phone.Text = "";
            TextBox_BirthDate.Text = "";
            TextBox_Policy.Text = "";

            DataTable dt = _dataBase.UpdateShow("select surename from polyclinic.patients");

            foreach (DataRow dr in dt.Rows)
            {
                ComboBox_SelectionPatient.Items.Add(dr[0]);
                ComboBox_Patient.Items.Add(dr[0]);
            }
        }

        private void Button_AddNote_Click(object sender, RoutedEventArgs e)
        {
            TabControl_All.SelectedItem = TabItem_Menu;
        }



        private void Button_ClosePatients_Click(object sender, RoutedEventArgs e)
        {
            TabControl_All.SelectedItem = TabItem_Menu;
        }
        private void Button_CloseMedCard_Click(object sender, RoutedEventArgs e)
        {
            TabControl_All.SelectedItem = TabItem_Patients;
        }
        private void Button_CloseMedicaments_Click(object sender, RoutedEventArgs e)
        {
            TabControl_All.SelectedItem = TabItem_Patients;
        }
        private void Button_CloseProcedures_Click(object sender, RoutedEventArgs e)
        {
            TabControl_All.SelectedItem = TabItem_Patients;
        }
        private void Button_CloseDiagnoses_Click(object sender, RoutedEventArgs e)
        {
            TabControl_All.SelectedItem = TabItem_Patients;
        }
        private void Button_CloseStatistics_Click(object sender, RoutedEventArgs e)
        {
            TabControl_All.SelectedItem = TabItem_Patients;
        }
        private void Button_CloseStatisticsAll_Click(object sender, RoutedEventArgs e)
        {
            TabControl_All.SelectedItem = TabItem_Patients;
        }
        private void Button_CloseSchedule_Click(object sender, RoutedEventArgs e)
        {
            TabControl_All.SelectedItem = TabItem_Doctors;
        }
        private void Button_CloseDoctors_Click(object sender, RoutedEventArgs e)
        {
            TabControl_All.SelectedItem = TabItem_Menu;
        }
        private void Button_CloseAddPatient_Click(object sender, RoutedEventArgs e)
        {
            TabControl_All.SelectedItem = TabItem_Patients;
        }
        private void Button_ClosePatientInfo_Click(object sender, RoutedEventArgs e)
        {
            TabControl_All.SelectedItem = TabItem_Patients;
        }


    }
}
