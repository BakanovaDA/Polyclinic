using Npgsql;
using System.Data;
using System.Windows;
using System;

namespace DatabaseLogic
{
    public class DataBase
    {

    }
    public class DB
    {
        private string _connString = "Host=localhost;Username=postgres;Password=bad-2001;Database=postgres";
        private NpgsqlConnection _conn; // отвечает за нашу БД
        private string _sql; // для приравнивания к ней sql-запросов
        private NpgsqlCommand _cmd; // чтобы выполнить sql-запрос внутри нашей БД
        private DataTable _dt; // чтобы получить данные с таблицы

        public DB()
        {
            _conn = new NpgsqlConnection(_connString);
        }

        public DataTable UpdateShow(string sql)
        {
            try
            {
                _conn.Open();

                _sql = @sql;
                _cmd = new NpgsqlCommand(_sql, _conn);
                _dt = new DataTable();
                _dt.Load(_cmd.ExecuteReader());

                _conn.Close();
            }
            catch (Exception ex)
            {
                _conn.Close();
                //MessageBox.Show(ex.Message);
            }

            return _dt;
        }

        public void AddPatient(string name, string surename, string patronymic, string address, long phone, string birthDate, long policy)
        {
            try
            {
                _conn.Open();

                _sql = @"select * from polyclinic.add_patient(@_name, @_surename, @_patronymic, @_address, @_phone, @_birthDate, @_policy)";
                _cmd = new NpgsqlCommand(_sql, _conn);

                _cmd.Parameters.AddWithValue("@_name", name);
                _cmd.Parameters.AddWithValue("@_surename", surename);
                _cmd.Parameters.AddWithValue("@_patronymic", patronymic);
                _cmd.Parameters.AddWithValue("@_address", address);
                _cmd.Parameters.AddWithValue("@_phone", phone);
                _cmd.Parameters.AddWithValue("@_birthDate", birthDate);
                _cmd.Parameters.AddWithValue("@_policy", policy);

                _cmd.ExecuteReader();

                _conn.Close();
            }
            catch (Exception ex)
            {
                _conn.Close();
                // MessageBox.Show(ex.Message);
            }
        }

        public DataTable GetPatientInfo(int id)
        {
            try
            {
                _conn.Open();

                _sql = @"select * from polyclinic.patients where (id_patient = @_id)";
                _cmd = new NpgsqlCommand(_sql, _conn);
                _cmd.Parameters.AddWithValue("@_id", id);
                _dt = new DataTable();
                _dt.Load(_cmd.ExecuteReader());

                _conn.Close();
            }
            catch (Exception ex)
            {
                _conn.Close();
                //MessageBox.Show(ex.Message);
            }

            return _dt;
        }

        public DataTable GetMedCard(int id)
        {
            try
            {
                _conn.Open();

                _sql = @"";
                _cmd = new NpgsqlCommand(_sql, _conn);
                _cmd.Parameters.AddWithValue("@_id", id);
                _dt = new DataTable();
                _dt.Load(_cmd.ExecuteReader());

                _conn.Close();
            }
            catch (Exception ex)
            {
                _conn.Close();
                //MessageBox.Show(ex.Message);
            }

            return _dt;
        }

        public DataTable GetMedicaments(int id)
        {
            try
            {
                _conn.Open();

                _sql = @"select name, surename, patronymic, name_medicament from polyclinic.medicaments inner join polyclinic.list_medicaments using(id_medicament) inner join polyclinic.admission_results using (list_number) inner join polyclinic.medical_cards using (id_medical_card) inner join polyclinic.patients using(id_patient) where (patients.id_patient = @_id)";
                _cmd = new NpgsqlCommand(_sql, _conn);
                _cmd.Parameters.AddWithValue("@_id", id);
                _dt = new DataTable();
                _dt.Load(_cmd.ExecuteReader());

                _conn.Close();
            }
            catch (Exception ex)
            {
                _conn.Close();
                //MessageBox.Show(ex.Message);
            }

            return _dt;
        }

        public DataTable GetProcedures(int id)
        {
            try
            {
                _conn.Open();

                _sql = @"select patients.name, surename, patronymic, checkups.name, date_of_appointment from polyclinic.coupons inner join polyclinic.patients using (id_patient) inner join polyclinic.checkups using (id_checkup) where (patients.id_patient = @_id)";
                _cmd = new NpgsqlCommand(_sql, _conn);
                _cmd.Parameters.AddWithValue("@_id", id);
                _dt = new DataTable();
                _dt.Load(_cmd.ExecuteReader());

                _conn.Close();
            }
            catch (Exception ex)
            {
                _conn.Close();
                //MessageBox.Show(ex.Message);
            }

            return _dt;
        }

        public DataTable GetDiagnoses(int id)
        {
            try
            {
                _conn.Open();

                _sql = @"select name, surename, patronymic, name_diagnose from polyclinic.diagnoses inner join polyclinic.admission_results using (id_diagnose) inner join polyclinic.medical_cards using (id_medical_card) inner join polyclinic.patients using(id_patient) where (patients.id_patient = @_id)";
                _cmd = new NpgsqlCommand(_sql, _conn);
                _cmd.Parameters.AddWithValue("@_id", id);
                _dt = new DataTable();
                _dt.Load(_cmd.ExecuteReader());

                _conn.Close();
            }
            catch (Exception ex)
            {
                _conn.Close();
                //MessageBox.Show(ex.Message);
            }

            return _dt;
        }

        public DataTable GetStatistics(int id)
        {
            try
            {
                _conn.Open();

                _sql = @"select name, surename, patronymic, count(id_diagnose) from polyclinic.medical_cards inner join polyclinic.admission_results using (id_medical_card) inner join polyclinic.patients using (id_patient) group by id_patient, name, surename, patronymic having (id_patient = @_id)";
                _cmd = new NpgsqlCommand(_sql, _conn);
                _cmd.Parameters.AddWithValue("@_id", id);
                _dt = new DataTable();
                _dt.Load(_cmd.ExecuteReader());

                _conn.Close();
            }
            catch (Exception ex)
            {
                _conn.Close();
                //MessageBox.Show(ex.Message);
            }

            return _dt;
        }

    }
}