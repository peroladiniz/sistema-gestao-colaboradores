using System.Collections.Generic;
using API_SIC_WEB_ANGULAR.Controllers;
using API_SIC_WEB_ANGULAR.DAL;
using API_SIC_WEB_ANGULAR.DTO;
using API_SIC_WEB_ANGULAR.Model;
using Microsoft.Extensions.Options;

namespace API_SIC_WEB_ANGULAR.Services
{
    public class BuscaOperadorService
    {
        private readonly BuscaOperadorDAL _buscaOperadorDAL;

        public BuscaOperadorService(BuscaOperadorDAL buscaOperadorDAL)
        {
            _buscaOperadorDAL = buscaOperadorDAL;
        }

        public async Task<List<BuscaOperadorResultado>> GetOperadoresService(string connStg, BuscaOperadorDTO parametro)
        {
            List<BuscaOperadorResultado> retornoObj = await _buscaOperadorDAL.GetOperadoresBuscaBanco(connStg, parametro);
            return retornoObj;
        }
        public async Task<List<BuscaOperadorResultadoFinal>> GetOperadoresResultadoFinalService(string connStg, string parametro)
        {
            return await _buscaOperadorDAL.GetOperadoresDetalhadosBanco(connStg, parametro);
        }

        public async Task<List<Funcao>> GetFuncoesService(string connStg)
        {
            return await _buscaOperadorDAL.GetFuncoesBanco(connStg);
        }
        public async Task<List<StatusOperador>> GetStatusFuncionarioService(string connStg)
        {
            return await _buscaOperadorDAL.GetStatusBanco(connStg);

        }
        public async Task<List<Perfil>> GetPerfilService(string connStg)
        {
            return await _buscaOperadorDAL.GetPerfilBanco(connStg);
        }
        public async Task<List<Cargo>> GetCargoService(string connStg)
        {
            return await _buscaOperadorDAL.GetCargoBanco(connStg);
        }
        public async Task<List<Expediente>> GetExpedienteService(string connStg, string matricula)
        {
            return await _buscaOperadorDAL.GetExpedienteBanco(connStg, matricula);
        }
        public async Task<List<Area>> GetAreaService(string connStg)
        {
            return await _buscaOperadorDAL.GetAreaBanco(connStg);
        }
        public async Task<List<CentroResultado>> GetCentroResultadoService(string connStg)
        {
            return await _buscaOperadorDAL.GetCentroResultadoBanco(connStg);
        }
        public async Task<List<TipoArea>> GetTipoAreaService(string connStg)
        {
            return await _buscaOperadorDAL.GetTipoAreaBanco(connStg);
        }
        public async Task<List<Chefia>> GetChefiaService(string connStg, string areCodigo, string site)
        {
            return await _buscaOperadorDAL.GetChefiaBanco(connStg, areCodigo, site);
        }
        public async Task<List<Site>> GetSiteService(string connStg)
        {
            return await _buscaOperadorDAL.GetSiteBanco(connStg);
        }
        public async Task<List<ConsultaEvento>> GetConsultaEventoService(string connStg)
        {
            return await _buscaOperadorDAL.GetConsultaEventoBanco(connStg);
        }
        public async Task<List<ConsultaEventoFuncionario>> GetConsultaEventoFuncionarioService(string connStg, string matricula)
        {
            return await _buscaOperadorDAL.GetConsultaEventoFuncionarioBanco(connStg, matricula);
        }
        public async Task<List<BuscaDatasOperador>> GetConsultaDatasOperadorService(string connStg, string matricula)
        {
            return await _buscaOperadorDAL.GetConsultaDatasOperador(connStg, matricula);
        }
        public async Task InserirOuAtualizarDatasOperadorService(string connStg, string matriculaLog, string matricula, DateTime? dataDemissao)
        {
            await _buscaOperadorDAL.InserirOuAtualizarDatasOperador(connStg, matriculaLog, matricula, dataDemissao);
        }
        public async Task<RecuperaCentroResultado> RecuperarCentroResultadoBancoService(string connStg, string nrReg, int? tipoOperacao)
        {
            return await _buscaOperadorDAL.GetCentroResultadoBancoAsync(connStg, nrReg, tipoOperacao);
        }
        public async Task GetRetornoAfastamentoBancoService(string connStg, RetornoAfastamento p_retornoAfastamento)
        {
            await _buscaOperadorDAL.GetRetornoAfastamentoBanco(connStg, p_retornoAfastamento);
        }

        /* Service que faz busca generalizada de eventos: */
        public async Task<List<ConsultaEventoFuncionario>> GetEventoAbertoService(string connStg, string matricula)
        {
            return await _buscaOperadorDAL.GetEventoAberto(connStg, matricula);
        }

        public async Task<List<ConsultaEventoFuncionario>> GetEventoAfastado(string connStg, string matricula)
        {
            List<ConsultaEventoFuncionario> obj_list = await this.GetEventoAbertoService(connStg, matricula);
            obj_list.RemoveAll(obj =>
                obj.EventoConsultaEventoFuncionario?.ToUpper() != "AFASTAMENTO" ||
                obj.Data_Retorno.HasValue
            );

            return obj_list;
        }
    }
}


