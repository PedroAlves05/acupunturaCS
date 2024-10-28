using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace NewAcupuntura.Entities
{
    public class Atendimento
    {
        [Key]
        public int Id { get; set; }

        public DateTime data { get; set; } = DateTime.Now;

        // Informações do Cliente
        public string? MoraComQuem { get; set; }
        public double Peso { get; set; }
        public double Altura { get; set; }

        // Fatores Pessoais e de Saúde
        public bool Filhos { get; set; }
        public bool Fumante { get; set; }
        public bool Vegetariano { get; set; }
        public bool Alcool { get; set; }
        public bool Drogas { get; set; }
        public bool AtividadeFisica { get; set; }
        public bool Meditacao { get; set; }
        public bool PressaoAlta { get; set; }
        public bool PressaoBaixa { get; set; }

        // Diagnóstico
        public string? Queixa { get; set; }
        public string? Doenca { get; set; }

        // Tratamento
        public string? TipoDeTratamento { get; set; } // Modificado para string
        public int QuantidadeSessoes { get; set; }
        public string? MetodoDePagamento { get; set; } // Modificado para string

        // Concordância
        public bool Concordo { get; set; }

        // Pontos Tratados
        public bool Olho { get; set; }
        public bool Olfato { get; set; }
        public bool Mandibular { get; set; }
        public bool Pulmoes { get; set; }
        public bool Auditivo { get; set; }
        public bool Estomago { get; set; }
        public bool Garganta { get; set; }
        public bool Gonadas { get; set; }
        public bool Pancreas { get; set; }
        public bool Coracao { get; set; }
        public bool Figado { get; set; }
        public bool Retal { get; set; }
        public bool Ciatico { get; set; }
        public bool Joelho { get; set; }
        public bool Rim { get; set; }
        public bool Trigemios { get; set; }
        public bool Agressividade { get; set; }
        public bool Tragus { get; set; }
        public bool Pele { get; set; }
        public bool Ombro { get; set; }
        public bool MembrosInferiores { get; set; }
        public bool MembrosSuperiores { get; set; }
        public bool Alergia { get; set; }
        public bool Darwin { get; set; }
        public bool Sintase { get; set; }
        public bool Talamo { get; set; }
        public bool Occipital { get; set; }
        public bool Genital { get; set; }
        public bool Medular { get; set; }

        public string? Observacao { get; set; }

        // Foreign key para a model Consulta
        [ForeignKey("Consulta")]
        public int ConsultaId { get; set; }
        public Consulta? Consulta { get; set; }
    }
}