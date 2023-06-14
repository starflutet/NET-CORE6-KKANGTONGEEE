using Dapper;
using Oracle.ManagedDataAccess.Client;
using System.Collections.Generic;
using System.Data;
using static ApiReperenceServer.Source.App.Constants;

namespace ApiReperenceServer.Source.App.DapperUtils
{
    /// <summary>
    /// 오라클 동적 매개 변수 목록
    /// </summary>
    public class OracleDynamicParameters : SqlMapper.IDynamicParameters
    {
        //////////////////////////////////////////////////////////////////////////////////////////////////// Field
        ////////////////////////////////////////////////////////////////////////////////////////// Private

        #region Field

        /// <summary>
        /// 동적 매개 변수 목록
        /// </summary>
        private readonly DynamicParameters dynamicParameters = new DynamicParameters();

        /// <summary>
        /// 오라클 매개 변수 리스트
        /// </summary>
        public List<OracleParameter> oracleParameterList = new List<OracleParameter>();

        #endregion

        //////////////////////////////////////////////////////////////////////////////////////////////////// Constructor
        ////////////////////////////////////////////////////////////////////////////////////////// Public

        #region 추가하기 - Add(name, value, dbType, direction, size)

        /// <summary>
        /// 추가하기
        /// </summary>
        /// <param name="name">명칭</param>
        /// <param name="value">값</param>
        /// <param name="dbType">DB 타입</param>
        /// <param name="direction">방향</param>
        /// <param name="size">크기</param>
        public void Add(string name, object value = null, DbType? dbType = null, ParameterDirection? direction = null, int? size = null)
        {
            this.dynamicParameters.Add(name, value, dbType, direction, size);
        }

        #endregion

        #region 추가하기 - Add(name, value, dbType, direction, size)

        /// <summary>
        /// 추가하기
        /// </summary>
        /// <param name="name">명칭</param>
        /// <param name="value">값</param>
        /// <param name="dbType">DB 타입</param>
        /// <param name="direction">방향</param>
        /// <param name="size">크기</param>
        public void AddIn(string name, object value = null, OracleDbType? dbType = null, ParameterDirection? direction = null, int? size = null)
        {

            this.dynamicParameters.Add(name, value, ConvertToDbType(dbType), direction, size);
        }

        private DbType? ConvertToDbType(OracleDbType? dbType)
        {
            switch (dbType)
            {
                case OracleDbType.Varchar2:
                    return DbType.String;
                case OracleDbType.Int32:
                    return DbType.Int32;
                case OracleDbType.Date:
                    return DbType.DateTime;
                // 다른 OracleDbType 값에 대한 변환을 추가할 수 있습니다.
                default:
                    throw new NotSupportedException($"OracleDbType '{dbType}' is not supported.");
            }
        }

        #endregion

        #region 추가하기 - Add(name, value, dbType, direction, size)

        /// <summary>
        /// 추가하기
        /// </summary>
        /// <param name="name">명칭</param>
        /// <param name="value">값</param>
        /// <param name="dbType">DB 타입</param>
        /// <param name="direction">방향</param>
        /// <param name="size">크기</param>
        public void AddOut(string name, OracleDbType oracleDbType, ParameterDirection direction, int size)
        {
            OracleParameter parameter = new OracleParameter(name, oracleDbType, size)
            {
                Direction = direction
            };

            this.oracleParameterList.Add(parameter);
        }
        #endregion

        #region 추가하기 - Add(name, oracleDbType, direction)

        /// <summary>
        /// 추가하기
        /// </summary>
        /// <param name="name">명칭</param>
        /// <param name="oracleDbType">오라클 DB 타입</param>
        /// <param name="direction">방향</param>
        public void AddOutRef(string name, OracleDbType oracleDbType, ParameterDirection direction)
        {
            OracleParameter parameter = new OracleParameter(name, oracleDbType, direction);

            this.oracleParameterList.Add(parameter);
        }

        #endregion

        #region (SqlMapper.IDynamicParameters) 매개 변수 목록 추가하기 - AddParameters(command, identity)

        /// <summary>
        /// 매개 변수 목록 추가하기
        /// </summary>
        /// <param name="command">명령</param>
        /// <param name="identity">식별자</param>
        public void AddParameters(IDbCommand command, SqlMapper.Identity identity)
        {
            ((SqlMapper.IDynamicParameters)this.dynamicParameters).AddParameters(command, identity);

            OracleCommand oracleCommand = command as OracleCommand;

            if (oracleCommand != null)
            {
                oracleCommand.Parameters.AddRange(this.oracleParameterList.ToArray());
            }
        }


        #endregion


    }

}
