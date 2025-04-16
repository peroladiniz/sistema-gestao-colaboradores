using System;
using System.Data;
using System.Data.SqlClient;
using API_SIC_WEB_ANGULAR.Controllers;
using API_SIC_WEB_ANGULAR.DTO;
using API_SIC_WEB_ANGULAR.Model;

namespace API_SIC_WEB_ANGULAR.DAL
{
    public class BuscaOperadorDAL
    {
        public async Task<List<BuscaOperadorResultado>> GetOperadoresBuscaBanco(string connStr, BuscaOperadorDTO objParam)
        {
            try
            {
                List<BuscaOperadorResultado> retornoResultado = new List<BuscaOperadorResultado>();
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    await conn.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandText = "STP_BUSCA_OPERADORES_PARA_API";

                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@MATRICULA", objParam.matricula);
                        cmd.Parameters.AddWithValue("@NOME", objParam.nome);
                        cmd.Parameters.AddWithValue("@CODINDI", objParam.codindi);
                        cmd.Parameters.AddWithValue("@MATRICULA_EQUIPE", objParam.matricula_equipe);
                        cmd.Parameters.AddWithValue("@CPF", objParam.cpf);
                        cmd.Parameters.AddWithValue("@FUNCAO", objParam.funcao);
                        cmd.Parameters.AddWithValue("@STATUS", objParam.status);
                        cmd.Parameters.AddWithValue("@CODIGO_ACESSO", objParam.codigo_acesso);
                        cmd.Parameters.AddWithValue("@LOGIN_EXTERNO", objParam.login_externo);
                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                BuscaOperadorResultado obj = new BuscaOperadorResultado();

                                obj.matricula = reader["MATRICULA"] == DBNull.Value ? "" : reader["MATRICULA"].ToString();
                                obj.nome = reader["NOME"] == DBNull.Value ? "" : reader["NOME"].ToString();
                                obj.sigla_status = reader["SIGLA_STATUS"] == DBNull.Value ? "" : reader["SIGLA_STATUS"].ToString();
                                obj.funcao = reader["FUNCAO"] == DBNull.Value ? "" : reader["FUNCAO"].ToString();
                                obj.perfil = reader["PERFIL"] == DBNull.Value ? "" : reader["PERFIL"].ToString();
                                obj.status = reader["STATUS"] == DBNull.Value ? "" : reader["STATUS"].ToString();
                                obj.acao = reader["ACAO"] == DBNull.Value ? "" : reader["ACAO"].ToString();
                                obj.superior = reader["SUPERIOR"] == DBNull.Value ? "" : reader["SUPERIOR"].ToString();
                                obj.numcpf = reader["NUMCPF"] == DBNull.Value ? "" : reader["NUMCPF"].ToString();
                                obj.acesso = reader["ACESSO"] == DBNull.Value ? "" : reader["ACESSO"].ToString();

                                retornoResultado.Add(obj);
                            }
                        }
                    }
                }
                return retornoResultado;
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<BuscaOperadorResultadoFinal>> GetOperadoresDetalhadosBanco(string connStr, string objParam)
        {
            List<BuscaOperadorResultadoFinal> retornoResultadoFinal = new List<BuscaOperadorResultadoFinal>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    await conn.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandText = "STP_CONSULTA_OPERADOR";
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@NRREG", objParam ?? (object)DBNull.Value);

                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                BuscaOperadorResultadoFinal objFinal = new BuscaOperadorResultadoFinal();

                                objFinal.nrreg = reader["NRREG"] as string ?? "";
                                objFinal.codindi = reader["CODINDI"] as string ?? "";
                                objFinal.cargo = reader["CARGO"] as string ?? "";
                                objFinal.codigo_cargo = reader["CODIGO_CARGO"] as string ?? "";
                                objFinal.funcao = reader["FUNCAO"] as string ?? "";
                                objFinal.sigla_funcao = reader["SIGLA_FUNCAO"] as string ?? "";
                                objFinal.perfil = reader["PERFIL"] as string ?? "";
                                objFinal.sigla_perfil = reader["SIGLA_PERFIL"] as string ?? "";
                                objFinal.status = reader["STATUS"] as string ?? "";
                                objFinal.sigla_status = reader["SIGLA_STATUS"] as string ?? "";
                                objFinal.statindi = reader["STATINDI"] as string ?? "";
                                objFinal.expediente = reader["EXPEDIENTE"] as string ?? "";
                                objFinal.codigo_expediente = reader["CODIGO_EXPEDIENTE"] as string ?? "";
                                objFinal.acesso = reader["ACESSO"] as string ?? "";
                                objFinal.acesso_a = reader["ACESSO_A"] as string ?? "";
                                objFinal.tpsal = reader["TPSAL"] as string ?? "";
                                objFinal.refeicao = reader["REFEICAO"] as string ?? "";
                                objFinal.vltrans = reader["VLTRANS"] as string ?? "";
                                objFinal.dataadmissao = reader["DATAADMISSAO"] as string ?? "";
                                objFinal.datademissao = reader["DATADEMISSAO"] as string ?? "";
                                objFinal.dataatualizacao = reader["DATAATUALIZACAO"] as string ?? "";
                                objFinal.site = reader["SITE"] as string ?? "";
                                objFinal.codigo_site = reader["CODIGO_SITE"] as string ?? "";
                                objFinal.acocodigo = reader["ACOCODIGO"] as string ?? "";
                                objFinal.acao = reader["ACAO"] as string ?? "";
                                objFinal.nome = reader["NOME"] as string ?? "";
                                objFinal.cpf = reader["CPF"] as string ?? "";
                                objFinal.rg = reader["RG"] as string ?? "";
                                objFinal.estadocivil = reader["ESTADOCIVIL"] as string ?? "";
                                objFinal.codigo_estadocivil = reader["CODIGO_ESTADOCIVIL"] as string ?? "";
                                objFinal.email = reader["EMAIL"] as string ?? "";
                                objFinal.datanascimento = reader["DATANASCIMENTO"] as string ?? "";
                                objFinal.telefone = reader["TELEFONE"] as string ?? "";
                                objFinal.celular = reader["CELULAR"] as string ?? "";
                                objFinal.endereco = reader["ENDERECO"] as string ?? "";
                                objFinal.bairro = reader["BAIRRO"] as string ?? "";
                                objFinal.cidade = reader["CIDADE"] as string ?? "";
                                objFinal.uf = reader["UF"] as string ?? "";
                                objFinal.cep = reader["CEP"] as string ?? "";
                                objFinal.tipo_login_externo = reader["TIPO_LOGIN_EXTERNO"] as string ?? "";
                                objFinal.login_externo = reader["LOGIN_EXTERNO"] as string ?? "";
                                objFinal.matricula_superior = reader["MATRICULA_SUPERIOR"] as string ?? "";
                                objFinal.nome_superior = reader["NOME_SUPERIOR"] as string ?? "";
                                objFinal.banco = reader["BANCO"] as string ?? "";
                                objFinal.codigo_banco = reader["CODIGO_BANCO"] as string ?? "";
                                objFinal.agencia = reader["AGENCIA"] as string ?? "";
                                objFinal.conta = reader["CONTA"] as string ?? "";
                                objFinal.codigo_area = reader["CODIGO AREA"] as string ?? "";
                                objFinal.area = reader["AREA"] as string ?? "";
                                objFinal.senha = reader["SENHA"] as string ?? "";
                                objFinal.pis = reader["PIS"] as string ?? "";
                                objFinal.equipe_abaixo = reader["EQUIPE_ABAIXO"] as string ?? "";
                                objFinal.cod_cargo = reader["COD_CARGO"] as string ?? "";
                                objFinal.prioridade_disc = reader["PRIORIDADE_DISC"] as string ?? "";

                                retornoResultadoFinal.Add(objFinal);
                            }
                        }
                    }
                }
                return retornoResultadoFinal;
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }
        internal async Task<List<Funcao>> GetFuncoesBanco(string connStg)
        {
            List<Funcao> listaFuncoes = new List<Funcao>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connStg))
                {
                    await conn.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandText = "STP_CONSULTA_FUNCAO";
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                Funcao funcao = new Funcao();
                                funcao.CodigoFuncao = reader["CODIGO"].ToString() ?? "";
                                funcao.DescricaoFuncao = reader["FUNCAO"].ToString() ?? "";

                                listaFuncoes.Add(funcao);
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }

            return listaFuncoes;
        }
        internal async Task<List<StatusOperador>> GetStatusBanco(string connStg)
        {
            List<StatusOperador> listaStatus = new List<StatusOperador>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connStg))
                {
                    await conn.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandText = "STP_CONSULTA_STATUS";
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                StatusOperador status = new StatusOperador();
                                status.CodigoStatus = reader["SIGLA"] == DBNull.Value ? "" : reader["SIGLA"].ToString();
                                status.DescricaoStatus = reader["STATUS"] == DBNull.Value ? "" : reader["STATUS"].ToString();

                                listaStatus.Add(status);
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }

            return listaStatus;
        }
        internal async Task<List<Perfil>> GetPerfilBanco(string connStg)
        {
            List<Perfil> listaPerfil = new List<Perfil>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connStg))
                {
                    await conn.OpenAsync();

                    using (SqlCommand cmd = new SqlCommand("STP_CONSULTA_PERFIL", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var perfil = new Perfil
                                {
                                    CodigoPerfil = reader["CODIGO"] as string ?? "",
                                    DescricaoPerfil = reader["PERFIL"] as string ?? ""
                                };

                                listaPerfil.Add(perfil);
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }

            return listaPerfil;
        }
        internal async Task<List<Cargo>> GetCargoBanco(string connStg)
        {
            List<Cargo> listaCargo = new List<Cargo>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connStg))
                {
                    await conn.OpenAsync();

                    using (SqlCommand cmd = new SqlCommand("STP_CONSULTA_CARGO", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var cargo = new Cargo
                                {
                                    CodigoCargo = reader["CODIGO"] as string ?? "",
                                    DescricaoCargo = reader["CARGO"] as string ?? ""
                                };

                                listaCargo.Add(cargo);
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }

            return listaCargo;
        }

        internal async Task<List<Expediente>> GetExpedienteBanco(string connStg, string matricula)
        {
            List<Expediente> listaExpediente = new List<Expediente>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connStg))
                {
                    await conn.OpenAsync();

                    using (SqlCommand cmd = new SqlCommand("STP_CONSULTA_EXPEDIENTE", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@MATRICULA", matricula);

                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                Expediente expediente = new Expediente
                                {
                                    CodigoExpediente = reader["CODIGO"] == DBNull.Value ? "" : reader["CODIGO"].ToString(),
                                    DescricaoExpediente = reader["EXPEDIENTE"] == DBNull.Value ? "" : reader["EXPEDIENTE"].ToString()
                                };

                                listaExpediente.Add(expediente);
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }

            return listaExpediente;
        }


        internal async Task<List<Area>> GetAreaBanco(string connStg)
        {
            List<Area> listaArea = new List<Area>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connStg))
                {
                    await conn.OpenAsync();

                    using (SqlCommand cmd = new SqlCommand("STP_CONSULTA_AREA", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                Area area = new Area();
                                area.CodigoArea = reader["CODIGO"] == DBNull.Value ? "" : reader["CODIGO"].ToString();
                                area.DescricaoArea = reader["AREA"] == DBNull.Value ? "" : reader["AREA"].ToString();

                                listaArea.Add(area);
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }

            return listaArea;
        }
        internal async Task<List<Chefia>> GetChefiaBanco(string connStg, string areCodigo, string site)
        {
            List<Chefia> listaChefia = new List<Chefia>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connStg))
                {
                    await conn.OpenAsync();

                    using (SqlCommand cmd = new SqlCommand("STP_CONSULTA_CHEFIA_AREA", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ARECODIGO", areCodigo);
                        cmd.Parameters.AddWithValue("@SITE", site);

                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                Chefia chefia = new Chefia
                                {
                                    CodigoChefia = reader["CODIGO"] == DBNull.Value ? "" : reader["CODIGO"].ToString(),
                                    DescricaoChefia = reader["DESCRICAO"] == DBNull.Value ? "" : reader["DESCRICAO"].ToString(),
                                    NomeChefia = reader["NOME"] == DBNull.Value ? "" : reader["NOME"].ToString()
                                };

                                listaChefia.Add(chefia);
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            return listaChefia;
        }

        internal async Task<List<CentroResultado>> GetCentroResultadoBanco(string connStg)
        {
            List<CentroResultado> listaCentroResultado = new List<CentroResultado>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connStg))
                {
                    await conn.OpenAsync();

                    using (SqlCommand cmd = new SqlCommand("STP_CONSULTA_CENTRORESULTADO", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;


                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                CentroResultado centroResultado = new CentroResultado
                                {
                                    CodigoCentroResultado = reader["CODIGO"] == DBNull.Value ? "" : reader["CODIGO"].ToString(),
                                    DescricaoCentroResultado = reader["DESCRICAO"] == DBNull.Value ? "" : reader["DESCRICAO"].ToString()
                                };

                                listaCentroResultado.Add(centroResultado);
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            return listaCentroResultado;
        }

        internal async Task<List<TipoArea>> GetTipoAreaBanco(string connStg)
        {
            List<TipoArea> listaTipoArea = new List<TipoArea>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connStg))
                {
                    await conn.OpenAsync();

                    using (SqlCommand cmd = new SqlCommand("STP_CONSULTA_TIPO_AREA", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                TipoArea tipoArea = new TipoArea
                                {
                                    CodigoTipoArea = reader["CODIGO"] == DBNull.Value ? "" : reader["CODIGO"].ToString(),
                                    DescricaoTipoArea = reader["DESCRICAO"] == DBNull.Value ? "" : reader["DESCRICAO"].ToString()
                                };

                                listaTipoArea.Add(tipoArea);
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }

            return listaTipoArea;
        }


        internal async Task<List<Site>> GetSiteBanco(string connStg)
        {
            List<Site> listaSite = new List<Site>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connStg))
                {
                    await conn.OpenAsync();

                    using (SqlCommand cmd = new SqlCommand("STP_CONSULTA_SITE", conn))
                    {
                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                Site site = new Site
                                {
                                    Codigo_Site = reader["CODIGO_SITE"] == DBNull.Value ? "" : reader["CODIGO_SITE"].ToString(),
                                    Sigla = reader["SIGLA"] == DBNull.Value ? "" : reader["SIGLA"].ToString(),
                                    Descricao = reader["DESCRICAO"] == DBNull.Value ? "" : reader["DESCRICAO"].ToString(),
                                };

                                listaSite.Add(site);
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception($"Erro ao consultar o banco de dados: {ex.Message}");
            }

            return listaSite;
        }

        internal async Task<List<ConsultaEvento>> GetConsultaEventoBanco(string connStg)
        {
            List<ConsultaEvento> listaConsultaEvento = new List<ConsultaEvento>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connStg))
                {
                    await conn.OpenAsync();

                    using (SqlCommand cmd = new SqlCommand("STP_CONSULTA_TIPO_EVENTOS_STATUS", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                ConsultaEvento consultaEvento = new ConsultaEvento
                                {
                                    CodigoConsultaEvento = reader["CODIGO"] == DBNull.Value ? "" : reader["CODIGO"].ToString(),
                                    EventoConsultaEvento = reader["EVENTO"] == DBNull.Value ? "" : reader["EVENTO"].ToString(),
                                };

                                listaConsultaEvento.Add(consultaEvento);
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception($"Erro ao consultar o banco de dados: {ex.Message}");
            }

            return listaConsultaEvento;
        }
        internal async Task<List<ConsultaEventoFuncionario>> GetConsultaEventoFuncionarioBanco(string connStg, string matricula)
        {
            List<ConsultaEventoFuncionario> listaConsultaEventoFuncionario = new List<ConsultaEventoFuncionario>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connStg))
                {
                    await conn.OpenAsync();

                    using (SqlCommand cmd = new SqlCommand("STP_CONSULTA_EVENTOS_FUNCIONARIO", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@NRREG", matricula);

                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                ConsultaEventoFuncionario consultaEventoFuncionario = new ConsultaEventoFuncionario
                                {

                                    EventoConsultaEventoFuncionario = reader["EVENTO"] == DBNull.Value ? "" : reader["EVENTO"].ToString(),
                                    Data_Saida = reader["DATA SAIDA"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["DATA SAIDA"]),
                                    Data_Retorno = reader["DATA RETORNO"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["DATA RETORNO"]),
                                    Data_Inclusao = reader["DATA INCLUSÃO"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["DATA INCLUSÃO"]),
                                    Matricula_Inclusao = reader["MATRICULA INCLUSÃO"] == DBNull.Value ? "" : reader["MATRICULA INCLUSÃO"].ToString(),
                                    Evn_Codigo = reader["EVN_CODIGO"] == DBNull.Value ? "" : reader["EVN_CODIGO"].ToString(),

                                };

                                listaConsultaEventoFuncionario.Add(consultaEventoFuncionario);
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception($"Erro ao consultar o banco de dados: {ex.Message}");
            }

            return listaConsultaEventoFuncionario;
        }
        internal async Task<List<BuscaDatasOperador>> GetConsultaDatasOperador(string connStg, string matricula)
        {
            List<BuscaDatasOperador> listaConsultaDatas = new List<BuscaDatasOperador>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connStg))
                {
                    await conn.OpenAsync();

                    using (SqlCommand cmd = new SqlCommand("STP_CONSULTAR_DATAS_OPERADOR", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@NRREG", matricula);

                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                BuscaDatasOperador consultaDatas = new BuscaDatasOperador
                                {
                                    DataAdmissao = reader["DTADMI"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["DTADMI"]),
                                    DataDemissao = reader["DTDEMI"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["DTDEMI"]),
                                    DataAtualizacao = reader["DTATUA"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["DTATUA"]),
                                };

                                listaConsultaDatas.Add(consultaDatas);
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception($"Erro ao consultar o banco de dados: {ex.Message}");
            }

            return listaConsultaDatas;
        }
        internal async Task InserirOuAtualizarDatasOperador(string connStg, string matriculaLog, string matricula, DateTime? dataDemissao)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connStg))
                {
                    await conn.OpenAsync();

                    using (SqlCommand cmd = new SqlCommand("STP_INSERIR_ATUALIZAR_DATAS_OPERADOR", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@NRREG_LOG", matriculaLog);
                        cmd.Parameters.AddWithValue("@NRREG", matricula);
                        cmd.Parameters.AddWithValue("@DATADEMISSAO", dataDemissao ?? (object)DBNull.Value);


                        await cmd.ExecuteNonQueryAsync();
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception($"Erro ao inserir ou atualizar os dados do operador: {ex.Message}");
            }
        }
        internal async Task<RecuperaCentroResultado> GetCentroResultadoBancoAsync(string connStg, string nrReg, int? tipoOperacao)
        {
            RecuperaCentroResultado ret_CentroResultado = new RecuperaCentroResultado();

            try
            {
                using (SqlConnection conn = new SqlConnection(connStg))
                {
                    await conn.OpenAsync();

                    using (SqlCommand cmd = new SqlCommand("STP_RECUPERA_CENTRO_RESULTADO_SIC_WEB", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;


                        cmd.Parameters.AddWithValue("@NRREG", nrReg);
                        cmd.Parameters.AddWithValue("@TIPO_OPERACAO", tipoOperacao.HasValue ? (object)tipoOperacao.Value : DBNull.Value);

                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                ret_CentroResultado = new RecuperaCentroResultado
                                {
                                    Codigo = reader["CODIGO"] == DBNull.Value ? "" : reader["CODIGO"].ToString(),
                                    CtrExterno = reader["CTR_EXTERNO"] == DBNull.Value ? "" : reader["CTR_EXTERNO"].ToString(),
                                    Descricao = reader["DESCRICAO"] == DBNull.Value ? "" : reader["DESCRICAO"].ToString()
                                };
                            }
                        }
                    }
                }
                return ret_CentroResultado;
            }
            catch (SqlException ex)
            {
                throw new Exception($"Erro em SQL ao recuperar centro de resultado: {ex.Message}");
            }
        }
        internal async Task GetRetornoAfastamentoBanco(string connStg, RetornoAfastamento p_retornoAfastamento)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connStg))
                {
                    await conn.OpenAsync();

                    using (SqlCommand cmd = new SqlCommand("STP_CADASTRAR_DATA_RETORNO", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@MATRICULA", p_retornoAfastamento.Matricula);
                        cmd.Parameters.AddWithValue("@MATRICULA_LOG", string.IsNullOrEmpty(p_retornoAfastamento.Matricula_Log) ? (object)DBNull.Value : p_retornoAfastamento.Matricula_Log);
                        cmd.Parameters.AddWithValue("@DTINI", p_retornoAfastamento.DtIni != DateTime.MinValue ? p_retornoAfastamento.DtIni : (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@DTFIM", p_retornoAfastamento.DtFim != null ? p_retornoAfastamento.DtFim : (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@SAIDA_RETORNO", p_retornoAfastamento.Saida_Retorno == 1 || p_retornoAfastamento.Saida_Retorno ==2 ? p_retornoAfastamento.Saida_Retorno : (object)DBNull.Value);

                        await cmd.ExecuteNonQueryAsync();
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception($"Erro SQL ao realizar retorno de afastamento: {ex.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro inesperado ao realizar retorno de afastamento: {ex.Message}");
            }
        }
        internal async Task<List<ConsultaEventoFuncionario>> GetEventoAberto(string connStg, string matricula)
        {
            try
            {
                List<ConsultaEventoFuncionario> ret_listaConsultaEvento = new List<ConsultaEventoFuncionario>();
                using (SqlConnection conn = new SqlConnection(connStg))
                {
                    await conn.OpenAsync();

                    using (SqlCommand cmd = new SqlCommand("STP_CONSULTA_EVENTOS_FUNCIONARIO", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@NRREG", matricula);

                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                //if (reader["DATA RETORNO"] == DBNull.Value && reader["EVENTO"] != DBNull.Value)
                                //{
                                ConsultaEventoFuncionario obj_ConsultaEventoFuncionario = new ConsultaEventoFuncionario
                                {
                                    EventoConsultaEventoFuncionario = reader["EVENTO"] == DBNull.Value ? "" : reader["EVENTO"].ToString(),
                                    Data_Saida = reader["DATA SAIDA"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["DATA SAIDA"]),
                                    Data_Retorno = reader["DATA RETORNO"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["DATA RETORNO"]),
                                    Data_Inclusao = reader["DATA INCLUSÃO"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["DATA INCLUSÃO"]),
                                    Matricula_Inclusao = reader["MATRICULA INCLUSÃO"] == DBNull.Value ? "" : reader["MATRICULA INCLUSÃO"].ToString(),
                                    Evn_Codigo = reader["EVN_CODIGO"] == DBNull.Value ? "" : reader["EVN_CODIGO"].ToString()
                                };
                                ret_listaConsultaEvento.Add(obj_ConsultaEventoFuncionario);
                                //}
                            }
                        }
                    }
                }
                return ret_listaConsultaEvento;
            }
            catch (SqlException ex)
            {
                throw new Exception($"Erro SQL ao consultar evento aberto: {ex.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro INESPERADO ao consultar evento aberto: {ex.Message}");
            }
        }
    }
}