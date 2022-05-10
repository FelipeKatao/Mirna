using System;
using Spectre.Console;

namespace Mirna
{
    public class testeClass{
       public  int VarA {get;set;}
       public  string VarB {get;set;}
    }
    class Program
    {

        //PADRONIZAR TODAS AS CLASSES DE ACESSO A BANCO DE DADOS!!
        static bool PrimeiroAcesso = false;
        public static string[] DynamicElements = new string[10];
        public static string Event = "";
        static void Main(string[] args)
        {
            testeClass Acd = new testeClass();
            Acd.VarB = "texto fixo";
            object Filter;
            dynamic filter2;
            string VarB;
            Filter = Acd;
            System.Console.WriteLine(Filter);
            filter2 = Acd;
            VarB = filter2.VarB;
            Title();
            string teste;
            PrimeiroAcesso = true;
            while (true)
                {
                    System.Console.WriteLine("Entre com uma ação");
                    teste = Console.ReadLine();
                    System.Console.WriteLine(SelectOption.SelectCommand(teste));
                    Console.ReadKey();
                    Console.Clear();
                    Title();
                }

        }
        public static void PrivateAcess(string[] Param)
        {
            Console.WriteLine(Param[0]+" Teste");
            System.Console.WriteLine(TesteStr());
        }
        private static string TesteStr(){
               return "Banco de dados lido com sucesso";
        }
        public static void Title()
        {
            AnsiConsole.Render(new FigletText("Mirna").Centered().Color(Color.Purple));
            if (PrimeiroAcesso == true)
            {
                System.Console.WriteLine(" ");
                //Se o cliente habilitar aparecer aqui outros menus interessantes como "QUERY EDITOR" onde obtem a ultima consulta realizada
               // BarSelectionBase();
            }
            else
            {
                AnsiConsole.Markup("[purple]Caso seja sua primeira vez execute --h para obter ajuda[/]");
                AnsiConsole.Markup("[purple]Seja bem vindo ao gerenciamento de dados Mirna[/]");
            }
            System.Console.WriteLine("  ");
        }
        public static void BarSelectionBassse()
        {
            string[] TitleTool = new string[] { "Untitled tool", "NOTHING SELECTED", "[purple]Select one Tool for inicialize the tool explorer[/]", "[blue]Wait inicialize tool[/]" };
            if (Event == "")
            {
                DynamicElements[0] = "Untitled tool";
                DynamicElements[1] = "NOTHING SELECTED";
                DynamicElements[2] = "[purple]Select one Tool for inicialize the tool explorer[/]";
                DynamicElements[3] = "[blue]Wait inicialize tool[/]";
            }
            else
            {
                for (int i = 0; i < DynamicElements.Length; i++)
                {
                    DynamicElements[i] = SelectOption.Providers[i];
                }
            }
            var table = new Table().BorderColor(Color.Purple).Border(TableBorder.DoubleEdge);
            table.AddColumn(DynamicElements[0]);
            table.AddColumn(new TableColumn(DynamicElements[1]).Centered());
            int IndexValue = 2;
            if (Event != "")
            {

                while (DynamicElements[IndexValue] != "")
                {
                    table.AddRow(new Markup(DynamicElements[IndexValue]), new Markup(DynamicElements[IndexValue + 1]));
                    IndexValue += 2;
                    if (DynamicElements[IndexValue] == null)
                    {
                        break;
                    }
                }
            }
            //Inserir aqui a ferramenta para inserir novas linhas se houver!
            table.Border(TableBorder.Rounded);
            table.Expand();
            table.Centered();
            AnsiConsole.Render(table);
        }
    }
}
