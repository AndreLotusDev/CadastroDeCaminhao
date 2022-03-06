using System;
using System.Collections.Generic;

namespace CadastroDeCaminhao.Dominio.Entidades
{
    public abstract class EntidadeBase
    {
        public Guid Id { get; set; }
        public DateTime? CriadoEm { get; set; }
        public string? CriadoPeloUsuarioId { get; set; }
        public DateTime? AtualizadoEm { get; set; }
        public string? AtualizadoPeloUsuarioId { get; set; }

        public abstract (bool ehValido, List<string> mensagensDeValidao) Validar();
    }
}
