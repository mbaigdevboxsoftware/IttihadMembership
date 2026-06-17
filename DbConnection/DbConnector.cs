using System.Data;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.Data.SqlClient;

namespace IttihadmembershipAPI.DbConnection
{
    public class DbConnector
    {

        private DbConnector()
        {
        }
        public static IConfigurationRoot GetConfiguration()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            return builder.Build();
        }
        private static string GetConnectionString()
        {
            var configuation = GetConfiguration();
            return configuation.GetSection("ConnectionStrings").GetSection("appserviceconnection").Value;
        }
        public static int ExecuteNonQuery(string cmdText, SqlParameter[] cmdParms)
        {
            SqlConnection conn = new SqlConnection(GetConnectionString());
            SqlCommand cmd = conn.CreateCommand();
            PrepareCommand(cmd, conn, null, CommandType.StoredProcedure, cmdText, cmdParms);
            int val = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            return val;

        }

        public static SqlDataReader ExecuteReader(string cmdText, SqlParameter[] cmdParms)
        {

            var conn = new SqlConnection(GetConnectionString());
            try
            {
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandTimeout = 0;
                PrepareCommand(cmd, conn, null, CommandType.StoredProcedure, cmdText, cmdParms);
                var dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                return dr;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string ExecuteDataSet(string cmdText, SqlParameter[] cmdParms)
        {
            string strXml = "";
            SqlConnection conn = new SqlConnection(GetConnectionString());
            SqlCommand cmd = conn.CreateCommand();
            PrepareCommand(cmd, conn, null, CommandType.StoredProcedure, cmdText, cmdParms);
            //var rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            //return rdr;
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                DataSet ds = new DataSet();
                da.Fill(ds);
                cmd.Parameters.Clear();
                XmlElement xE = (XmlElement)Serialize(ds);
                strXml = xE.OuterXml.ToString();
            }
            conn.Close();
            return strXml;
        }

        public static XmlElement Serialize(object transformObject)
        {
            XmlElement serializedElement = null;
            try
            {
                MemoryStream memStream = new MemoryStream();
                XmlSerializer serializer = new XmlSerializer(transformObject.GetType());
                serializer.Serialize(memStream, transformObject);
                memStream.Position = 0;
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(memStream);
                serializedElement = xmlDoc.DocumentElement;
            }
            catch (Exception SerializeException)
            {
            }
            return serializedElement;
        }
        public static object ExecuteScalar(string cmdText, SqlParameter[] cmdParms)
        {
            SqlConnection conn = new SqlConnection(GetConnectionString());
            SqlCommand cmd = conn.CreateCommand();
            PrepareCommand(cmd, conn, null, CommandType.StoredProcedure, cmdText, cmdParms);
            object val = cmd.ExecuteScalar();
            cmd.Parameters.Clear();
            return val;
        }

        private static void PrepareCommand(SqlCommand cmd, SqlConnection conn, SqlTransaction trans, CommandType cmdType, string cmdText, SqlParameter[] commandParameters)
        {
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            if (trans != null)
            {
                cmd.Transaction = trans;
            }
            cmd.CommandType = cmdType;
            //attach the command parameters if they are provided
            if (commandParameters != null)
            {
                AttachParameters(cmd, commandParameters);
            }
        }
        private static void AttachParameters(SqlCommand command, SqlParameter[] commandParameters)
        {
            foreach (SqlParameter p in commandParameters)
            {
                //check for derived output value with no value assigned
                if ((p.Direction == ParameterDirection.InputOutput) && (p.Value == null))
                {
                    p.Value = DBNull.Value;
                }
                command.Parameters.Add(p);
            }
        }
    }
}
