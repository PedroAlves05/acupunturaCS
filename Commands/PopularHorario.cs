using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NewAcupuntura.Entities;

namespace NewAcupuntura.Commands
{
    public class PopularHorario
    {
        private readonly AcupunturaDbContext _context;

        public PopularHorario(AcupunturaDbContext context)
        {
            _context = context;
        }

        public async Task PopularHorariosAsync(int numeroDeDias, int intervaloDeTempo)
        {
            // Data inicial
            var dataInicial = DateTime.Now.Date;

            for (int dia = 0; dia < numeroDeDias; dia++)
            {
                var data = dataInicial.AddDays(dia);

                // Verifica se o dia é sábado (6) ou domingo (0)
                bool disponivel = data.DayOfWeek != DayOfWeek.Saturday && data.DayOfWeek != DayOfWeek.Sunday;

                // Começa às 13:00 e termina às 18:00
                var hora = new TimeSpan(13, 0, 0);
                var horaFinal = new TimeSpan(18, 0, 0);

                while (hora < horaFinal)
                {
                    // Verifica se já existe um horário cadastrado
                    if (!await _context.Horarios.AnyAsync(h => h.Data.Date == data && h.Disponivel == disponivel))
                    {
                        // Define disponível como False se o horário estiver entre 12:00 e 13:00
                        if ((hora >= new TimeSpan(12, 0, 0) && hora < new TimeSpan(13, 0, 0)) ||
                            data.DayOfWeek == DayOfWeek.Saturday || data.DayOfWeek == DayOfWeek.Sunday)
                        {
                            disponivel = false;
                        }

                        // Cria um novo objeto Horario com base na nova model
                        var horario = new Horario
                        {
                            Data = data.Add(hora), // Adiciona o horário à data
                            Disponivel = disponivel,
                        };

                        await _context.Horarios.AddAsync(horario);
                    }

                    // Incrementa o horário por 15 minutos
                    hora = hora.Add(new TimeSpan(0, intervaloDeTempo, 0));
                }
            }

            // Salva as alterações no banco de dados
            await _context.SaveChangesAsync();
        }
    }
}
