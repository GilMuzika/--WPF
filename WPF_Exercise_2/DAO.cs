using Microsoft.Data.Sqlite;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WPF_Exercise_2
{
    enum AdditionalDataFlags
    {
        totalPurchasesSumForEveryClient = 0, //finding customers
        findProductPlainly = 1, //finding product
        findProductWithAdditionalData = 2, //finding products
        noFlagTerminator = 1000 //also finding customers
    }

    class DAO
    {
        private SqliteConnection _connection = new SqliteConnection();
        private SqliteCommand _command = new SqliteCommand();

        public DAO()
        {
            _connection.ConnectionString = ConfigurationManager.ConnectionStrings["connstr"].ConnectionString.Replace(@"=\", $"={Directory.GetCurrentDirectory()}\\");

            _command.CommandType = System.Data.CommandType.Text;
            _command.Connection = _connection;
        }
        public void Add<T>(T obj)
        {
            InsertValueToTable(obj, typeof(T).Name);
        }

        public void Update<T>(T obj)
        {
            UpdateValueInTable(obj, typeof(T).Name);
        }
        private void UpdateValueInTable(object valueForUpdating, string tableName)
        {
            _connection.Open();
            Action act = () =>
            {
                //_command.CommandText = $"INSERT INTO {tableName} ({valueForUpdating.GetType().GetProperties()[0].Name}) VALUES ('{valueForUpdating.GetType().GetProperties()[0].GetValue(valueForUpdating)}')";
                //_command.ExecuteNonQuery();

                for (int i = 1; i < valueForUpdating.GetType().GetProperties().Length; i++)
                {
                    var val = valueForUpdating.GetType().GetProperties()[i].GetValue(valueForUpdating);
                    if (!Int32.TryParse(val.ToString(), out int numVal)) val = $"'{val}'";
                    _command.CommandText = $"UPDATE {tableName} SET {valueForUpdating.GetType().GetProperties()[i].Name} = {val} WHERE {valueForUpdating.GetType().GetProperties()[0].Name} = '{valueForUpdating.GetType().GetProperties()[0].GetValue(valueForUpdating)}'";
                    _command.ExecuteNonQuery();
                }
            };
            ProcessExceptions(act);
            _connection.Close();
        }

        private void InsertValueToTable(object insertValueHoldingObject, string tableName)
        {
            _connection.Open();
            Action act = () =>
            {
                _command.CommandText = $"INSERT INTO {tableName} ({insertValueHoldingObject.GetType().GetProperties()[0].Name}) VALUES ('{insertValueHoldingObject.GetType().GetProperties()[0].GetValue(insertValueHoldingObject)}')";
                _command.ExecuteNonQuery();

                for (int i = 1; i < insertValueHoldingObject.GetType().GetProperties().Length; i++)
                {
                    var val = insertValueHoldingObject.GetType().GetProperties()[i].GetValue(insertValueHoldingObject);
                    if (!Int32.TryParse(val.ToString(), out int numVal)) val = $"'{val}'";
                    _command.CommandText = $"UPDATE {tableName} SET {insertValueHoldingObject.GetType().GetProperties()[i].Name} = {val} WHERE {insertValueHoldingObject.GetType().GetProperties()[0].Name} = '{insertValueHoldingObject.GetType().GetProperties()[0].GetValue(insertValueHoldingObject)}'";
                    _command.ExecuteNonQuery();
                }
            };
            ProcessExceptions(act);
            _connection.Close();
        }

        public void Delete<T>(T obj)
        {
            DeleteFromTable(obj, typeof(T).Name);
        }
        private void DeleteFromTable(object toDelete, string tableName)
        {
            Action act = () =>
            {
                _connection.Open();
                _command.CommandText = $"DELETE FROM {tableName} WHERE ID = {toDelete.GetType().GetProperties()[0].GetValue(toDelete)}";
                _command.ExecuteNonQuery();
                _connection.Close();
            };
            ProcessExceptions(act);
        }

        private T DictToType<T>(Dictionary<string, Object> dict) where T : class, new()
        {
            T obj = new T();
            foreach(var s in typeof(T).GetProperties())
            {
                bool flg = dict.TryGetValue(s.Name, out object val);
                var r = val.GetType().Name;
                if (!(val is DBNull)) 
                {
                    if (flg) s.SetValue(obj, val);
                    else throw new Exception($"properties of your type \"{typeof(T).Name}\" aren't corresponding to the values of the input dictionary. Please use compatible types.");
                }
            }
            return obj;
        }

        public List<T> GetAll<T>() where T : class, new()
        {
            string tableName = typeof(T).Name;            
            List<Dictionary<string, Object>> dictList = RetriveFromTableInternal(tableName, -2, AdditionalDataFlags.noFlagTerminator);
            return dictList.Select(x => DictToType<T>(x)).ToList();


        }
        public T GetByID<T>(int ID) where T : class, new()
        {
            string tableName = typeof(T).Name;
            List<Dictionary<string, Object>> dictList = RetriveFromTableInternal(tableName, ID, AdditionalDataFlags.noFlagTerminator);
            return dictList.Select(x => DictToType<T>(x)).FirstOrDefault();
        }
        public List<Dictionary<string, Object>> RetriveAllFromTableWithAdditionalData(string tableName, int ID, AdditionalDataFlags additionalDataFlag)
        {
            return RetriveFromTableInternal(tableName, ID, additionalDataFlag);
        }

        private List<Dictionary<string, Object>> RetriveFromTableInternal(string tableName, int id, AdditionalDataFlags additionalDataFlag)
        {
            List<Dictionary<string, Object>> ret = new List<Dictionary<string, Object>>();
            Action act = () =>
            {

                _connection.Open();
                if (id > -1) _command.CommandText = $"SELECT * FROM {tableName} WHERE ID = {id}";
                else _command.CommandText = $"SELECT * FROM {tableName}";
                using (SqliteDataReader reader = _command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var dict = BuildDictionary(reader);

                        ret.Add(dict);

                        if (id > -1) break;
                    }
                };
                _connection.Close();
            };
            ProcessExceptions(act);
            return ret;
        }

        private Dictionary<string, Object> BuildDictionary(SqliteDataReader reader)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            int count = 0;
            while (count < reader.FieldCount)
            {
                dict.Add(reader.GetName(count), reader.GetValue(count));
                count++;
            }
            return dict;
        }







        public void ProcessExceptions(Action act)
        {
            try
            {
                act();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.GetType().Name}\n\n{ex.Message}\n\n\n{ex.StackTrace}");
            }
        }




    }


    
}
