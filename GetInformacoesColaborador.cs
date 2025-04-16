using Microsoft.AspNetCore.Mvc;
using API_SIC_WEB_ANGULAR.Model;
using API_SIC_WEB_ANGULAR.Services;
using API_SIC_WEB_ANGULAR.DTO;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Threading.Tasks;
using System.Security.Cryptography.X509Certificates;
using API_SIC_WEB_ANGULAR.DAL;
namespace API_SIC_WEB_ANGULAR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class GetInformacoesColaborador : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly PermissaoDeAcessoEloginAntigoService _service;
        private readonly BuscaOperadorService _buscaOperadorService;

        public GetInformacoesColaborador(PermissaoDeAcessoEloginAntigoService service, BuscaOperadorService buscaOperadorService, IConfiguration configuration)
        {
            _service = service;
            _buscaOperadorService = buscaOperadorService;
            _configuration = configuration;
        }



        [HttpGet("GetInfos")]
        public async Task<ColaboradorInfos> GetInfosColaborador(string matricula)
        {
            ColaboradorInfos colaboradores = await _service.EnviaMatriculaColaborador(matricula);
            return colaboradores;
        }

        [HttpPost("DesbloqueioOperador")]
        public async Task<IActionResult> DesbloquearOperador([FromBody] ColaboradorInfos desbloqueioOperador)
        {
            if (await _service.ConfirmaDesbloqueioOperador(desbloqueioOperador))
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet("BuscarOperadores")]
        public async Task<IActionResult> BuscarOperadores(string? matricula = "", string? nome = "", string? codindi = "", string? matricula_equipe = "", string? cpf = "", string? funcao = "", string? status = "", string? codigo_acesso = "", string? login_externo = "")
        {
            try
            {
                BuscaOperadorDTO obj = new BuscaOperadorDTO
                {
                    matricula = matricula,
                    nome = nome,
                    codindi = codindi,
                    matricula_equipe = matricula_equipe,
                    cpf = cpf,
                    funcao = funcao,
                    status = status,
                    codigo_acesso = codigo_acesso,
                    login_externo = login_externo
                };

                List<BuscaOperadorResultado> objResultado = await this._buscaOperadorService.GetOperadoresService(_configuration.GetConnectionString("SicWeb"), obj);

                if (objResultado.Count() > 0)
                {
                    return Ok(objResultado);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("BuscarOperadorResultadoFinal")]
        public async Task<IActionResult> BuscarOperadorResultadoFinal(string matricula)
        {
            try
            {
                List<BuscaOperadorResultadoFinal> resultados = await _buscaOperadorService.GetOperadoresResultadoFinalService(_configuration.GetConnectionString("SicWeb"), matricula);
                if (resultados.Count > 0)
                {
                    return Ok(resultados);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("BuscarFuncoes")]
        public async Task<IActionResult> BuscarFuncoes()
        {
            try
            {
                List<Funcao> funcoes = await _buscaOperadorService.GetFuncoesService(_configuration.GetConnectionString("SicWeb"));

                if (funcoes.Count > 0)
                {
                    return Ok(funcoes);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("BuscarStatus")]
        public async Task<IActionResult> BuscarStatus()
        {
            try
            {
                List<StatusOperador> status = await _buscaOperadorService.GetStatusFuncionarioService(_configuration.GetConnectionString("SicWeb"));

                if (status.Count > 0)
                {
                    return Ok(status);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("BuscarPerfil")]
        public async Task<IActionResult> BuscarPerfil()
        {
            try
            {
                List<Perfil> perfis = await _buscaOperadorService.GetPerfilService(_configuration.GetConnectionString("SicWeb"));

                if (perfis.Count > 0)
                {
                    return Ok(perfis);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpGet("BuscarCargo")]
        public async Task<IActionResult> BuscarCargo()
        {
            try
            {
                List<Cargo> cargos = await _buscaOperadorService.GetCargoService(_configuration.GetConnectionString("SicWeb"));

                if (cargos.Count > 0)
                {
                    return Ok(cargos);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("BuscarExpediente")]
        public async Task<IActionResult> BuscarExpediente(string matricula)
        {
            try
            {
                List<Expediente> expediente = await _buscaOperadorService.GetExpedienteService(_configuration.GetConnectionString("SicWeb"), matricula);

                if (expediente.Count > 0)
                {
                    return Ok(expediente);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpGet("BuscarArea")]
        public async Task<IActionResult> BuscarArea()
        {
            try
            {
                List<Area> area = await _buscaOperadorService.GetAreaService(_configuration.GetConnectionString("SicWeb"));

                if (area.Count > 0)
                {
                    return Ok(area);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("BuscarChefia")]
        public async Task<IActionResult> BuscarChefia([FromQuery] string areCodigo, [FromQuery] string site)
        {
            try
            {
                if (string.IsNullOrEmpty(areCodigo) || string.IsNullOrEmpty(site))
                {
                    return BadRequest("Parâmetros 'areCodigo' e 'site' são obrigatórios.");
                }

                List<Chefia> ret_ListChefia = await this._buscaOperadorService.GetChefiaService(_configuration.GetConnectionString("SicWeb"), areCodigo, site);

                if (ret_ListChefia.Any())
                {
                    return Ok(ret_ListChefia);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {

                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }

        [HttpGet("BuscarCentroResultado")]
        public async Task<IActionResult> BuscarCentroResultado()
        {
            try
            {
                List<CentroResultado> centroResultado = await _buscaOperadorService.GetCentroResultadoService(_configuration.GetConnectionString("SicWeb"));

                if (centroResultado.Count > 0)
                {
                    return Ok(centroResultado);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("BuscarTipoArea")]
        public async Task<IActionResult> BuscarTipoArea()
        {
            try
            {
                List<TipoArea> tipoArea = await _buscaOperadorService.GetTipoAreaService(_configuration.GetConnectionString("SicWeb"));

                if (tipoArea.Count > 0)
                {
                    return Ok(tipoArea);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("BuscarSite")]
        public async Task<IActionResult> BuscarSite()
        {
            try
            {
                List<Site> ret_ListSite = await _buscaOperadorService.GetSiteService(_configuration.GetConnectionString("SicWeb"));

                if (ret_ListSite.Any())
                {
                    return Ok(ret_ListSite);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }

        [HttpGet("BuscarConsultaEvento")]
        public async Task<IActionResult> BuscarConsultaEvento()
        {
            try
            {
                List<ConsultaEvento> ret_ListConsultaEvento = await this._buscaOperadorService.GetConsultaEventoService(
                    _configuration.GetConnectionString("SicWeb"));

                if (ret_ListConsultaEvento.Any())
                {
                    return Ok(ret_ListConsultaEvento);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }

        [HttpGet("BuscarConsultaEventoFuncionario")]
        public async Task<IActionResult> BuscarConsultaEvento(string matricula)
        {
            try
            {

                if (string.IsNullOrEmpty(matricula))
                {
                    return BadRequest("A matrícula é obrigatória.");
                }

                List<ConsultaEventoFuncionario> ret_ListConsultaEventoFuncionario = await this._buscaOperadorService.GetConsultaEventoFuncionarioService(
                    _configuration.GetConnectionString("SicWeb"), matricula);

                if (ret_ListConsultaEventoFuncionario.Any())
                {
                    return Ok(ret_ListConsultaEventoFuncionario);
                }
                else
                {
                    return NotFound(new { message = "Nenhum evento encontrado para a matrícula informada." });
                }
            }
            catch (Exception ex)
            {

                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }

        [HttpGet("BuscarDatasOperador")]
        public async Task<IActionResult> BuscarDatasOperador(string matricula)
        {
            try
            {
                List<BuscaDatasOperador> ret_ListDatas = await _buscaOperadorService.GetConsultaDatasOperadorService(_configuration.GetConnectionString("SicWeb"), matricula);

                if (ret_ListDatas.Any())
                {
                    return Ok(ret_ListDatas);
                }
                else
                {
                    return NotFound("Operador não encontrado.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }

        [HttpPost("InserirOuAtualizarDatasOperador")]
        public async Task<IActionResult> InserirOuAtualizarDatasOperador(string matriculaLog, string matricula, DateTime? dataDemissao)
        {
            try
            {

                await _buscaOperadorService.InserirOuAtualizarDatasOperadorService(_configuration.GetConnectionString("SicWeb"), matriculaLog, matricula, dataDemissao);

                return Ok("Operador atualizado ou inserido com sucesso.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }

        [HttpGet("RecuperarCentroResultado")]
        public async Task<IActionResult> RecuperarCentroResultado(string nrReg, int? tipoOperacao)
        {
            try
            {
                RecuperaCentroResultado resultado = await _buscaOperadorService.RecuperarCentroResultadoBancoService(_configuration.GetConnectionString("SicWeb"), nrReg, tipoOperacao);
                if (resultado == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(resultado);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }

        [HttpPost("RecuperarRetornoAfastamento")]
        public async Task<IActionResult> RecuperarRetornoAfastamento(RetornoAfastamento p_retornoAfastamento)
        {
            p_retornoAfastamento.DtFim = p_retornoAfastamento.Saida_Retorno == 1 ? new DateTime(1900, 1, 1) : p_retornoAfastamento.DtFim;

            try
            {
                await _buscaOperadorService.GetRetornoAfastamentoBancoService(_configuration.GetConnectionString("SicWeb"),p_retornoAfastamento);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }
        [HttpGet("RecuperarAfastamento")]
        public async Task<IActionResult> RecuperarAfastamento(string matricula)
        {
            try
            {
                if (string.IsNullOrEmpty(matricula))
                {
                    return BadRequest("A matrícula é obrigatória.");
                }
                List<ConsultaEventoFuncionario> eventoAberto = await _buscaOperadorService.GetEventoAfastado(_configuration.GetConnectionString("SicWeb"), matricula);
                if (eventoAberto.Any())
                {
                    return Ok(eventoAberto); 
                }
                else
                {
                    return NotFound(new { message = "Nenhum evento aberto encontrado para a matrícula informada." });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Erro interno: {ex.Message}" });
            }
        }
    }
}
