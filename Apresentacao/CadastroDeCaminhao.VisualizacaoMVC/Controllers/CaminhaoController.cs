using CadastroDeCaminhao.Aplicacao.DTO;
using CadastroDeCaminhao.Aplicacao.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CadastroDeCaminhao.VisualizacaoMVC.Controllers
{
    [Authorize]
    public class CaminhaoController : Controller
    {
        private ICaminhaoAppService _caminhaoAppService { get; }
        public CaminhaoController(ICaminhaoAppService caminhaoAppService)
        {
            _caminhaoAppService = caminhaoAppService;
        }

        public async Task<IActionResult> Index()
        {
            var resultado = await _caminhaoAppService.PegaCaminhaoPorPaginacaoAsync(1, 100);

            return View(resultado.paginacaoDeCaminhaoDTO.Itens);
        }

        public async Task<IActionResult> RegistrarUmNovoCaminhao()
        {
            return await Task.Run(() => View());
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar(CaminhaoCriacaoDTO caminhaoDTO)
        {
            if (!ModelState.IsValid)
            {
                return View(nameof(RegistrarUmNovoCaminhao) ,caminhaoDTO);
            }

            var caminhaoCriado = await _caminhaoAppService.CriaCaminhaoAsync(caminhaoDTO);

            if(caminhaoCriado.criadoComSucesso)
            {
                return RedirectToAction(nameof(Index));
            }

            return BadRequest();
        }

        public async Task<IActionResult> EditarCaminhao(Guid id)
        {
            var caminhaoEncontrado = await _caminhaoAppService.PegaCaminhaoPorIdAsync(id);

            return await Task.Run(() => View(caminhaoEncontrado.caminhaoDTOParaRetornar));
        }

        public async Task<IActionResult> PostarEdicaoCaminhao(CaminhaoAtualizacaoDTO caminhaoDTO)
        {
            if (!ModelState.IsValid)
            {
                return View(nameof(EditarCaminhao), caminhaoDTO);
            }

            var caminhaoEditado = await _caminhaoAppService.AtualizaCaminhaoAsync(caminhaoDTO);

            if (caminhaoEditado.atualizadoComSucesso)
            {
                return RedirectToAction(nameof(Index));
            }

            return BadRequest();
        }

        public async Task<IActionResult> DeletarCaminhao(Guid id)
        {
            await _caminhaoAppService.DeletaCaminhaoPorIdAsync(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
