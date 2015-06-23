using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

namespace A2_NWBA.Code.Utils
{
    public class SqlParamsColl
    {
        Dictionary<string, SqlParameter> sqlParams;

        public SqlParamsColl()
        {
            sqlParams = new Dictionary<string, SqlParameter>();
        }

        public void Add(string Name, SqlDbType paramType, object value)
        {
            SqlParameter param = new SqlParameter(Name, paramType);
            param.Value = value;
            sqlParams.Add(param.ParameterName, param);
        }

        public static implicit operator SqlParameter[](SqlParamsColl sqlParams)
        {
            return sqlParams.ToArray();
        }

        public SqlParameter[] ToArray()
        {
            return sqlParams.Values.ToArray();
        }
    }
}