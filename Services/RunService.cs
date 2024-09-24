using microTrading.dto;
using microTrading.Models;
using microTrading.RepositoriesEF;

namespace microTrading.Services
{
    public class RunService
    {
        ActiveRepository _activeRepository;
        RunRepository _runRepository;
        public RunService(ActiveRepository activeRepository, RunRepository runRepository) {
            _activeRepository = activeRepository;
            _runRepository = runRepository;
        }

        public void LaunchRun(LaunchRunDto dto) {

            //Active active = _activeRepository.getOneFromSymbol(dto.Symbol);
            // RunPerfomance run = _runRepository.AddSingleFromSymbol(active);

            //Task.Run => permet de lancer une tâche en arrière plan et que le controller ne s'en occuppe plus
            //Task.Run créé un threas séparé, je voudrais que tous les appels API soient gérés dans un seul thread.
            //Créer un tableau d'actifs dans le service pour garder une trace des actifs que l'on suit, 
            //on peut remplir et vider le tableau avec un appel différent (LaunchRun prend un tableau et créé un thread
            //, ne peux pas créer un deuxième thread)
            // AddActiveToRun Remove------- permet de mettre et d'enlever un actif au thread.
            // Thread=> Instance of the application=> le thread séparé gère les runs
            // Run => Entité de la base de données qui rassemblent des données liées à un seul actif sur une période continue.
            // Quand on récupère des données pour la première fois avec ce thread sur un actif en ouvre un nouveau Run, quand
            // quand on cesse de récupérer des données on ferme le Run
            Task.Run(()=> testTimeOut(dto.Symbol));
        
        }

        void testTimeOut(string message)
        {
            while (true)
            {
                Console.WriteLine(message);
                Thread.Sleep(1000); // Wait for 1 second (1000 milliseconds)
            }
        }
    }
}
