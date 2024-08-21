using ScreenSound.Banco;
using ScreenSound.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenSound.Menus
{
    internal class MenuMostrarMusicasPorAno : Menu
    {
        public override void Executar(DAL<Artista> artistaDAL)
        {
            base.Executar(artistaDAL);
            ExibirTituloDaOpcao("Exibir musicas por ano de lançamento");

            var musicaDAL = new DAL<Musica>(new ScreenSoundContext());

            List<Musica> musicas = musicaDAL.SelectBy(m => m.AnoLancamento is not null).OrderBy(m => m.AnoLancamento).ToList();

            foreach (var musica in musicas)
            {
                Console.WriteLine($"{musica.AnoLancamento}, {musica.Nome}" + (musica.Artista is not null ? $" por {musica.Artista.Nome}": ""));
            }

            Console.WriteLine("\nDigite uma tecla para voltar ao menu principal");
            Console.ReadKey();
            Console.Clear();
        }
    }
}
