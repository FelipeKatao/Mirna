using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Mirna
{
    public static class SelectOption
    {
        //Manipulations off CLI
        public delegate void Calls();
        public static string[] Providers = new string[10];
        private static SourceTools Stools = new SourceTools();
        private static Calls _Del_Read = new Calls(ReadAllDocument);
        private static Calls _Del_Close = new Calls(CloseApp);
        private static Calls _Del_Create = new Calls(CreateNewConnection);
        private static Calls _Del_Helper = new Calls(Helper);
        private static Calls _Del_options = new Calls(showConfiguration);
        private static Calls _Del_alterCon = new Calls(ModifyDatas);
        private static Calls _Del_QueryCon = new Calls(ExecuteQuery);
        private static Calls _Del_Tool_List = new Calls(ListTool);
        private static Calls _Del_Tool_ListClear = new Calls(ClearTool);
        private static Calls _Del_Tool_Insert = new Calls(InsertDocument);
        private static Calls _Del_Tool_Where = new Calls(ReadWhereValue);
        private static Calls _Del_Tool_Where_Column = new Calls(ReadColumnWhereValue);
        private static Calls _Del_Tool_Update = new Calls(updateFields);
        public static string[] COMMANDS = new string[] { "CLOSE", "SELECT", "CREATE CON", "--H", "--CF", "ALTER CON", "QUERY", "TOOL LIST", "TOOL CLS", "INSERT", "DELETE", "WHERE VALUE", "WHERE VALUE COLUMN","UPDATE" };
        public static dynamic[] _options_call = { _Del_Close, _Del_Read, _Del_Create, _Del_Helper, _Del_options, _Del_alterCon, _Del_QueryCon, _Del_Tool_List, _Del_Tool_ListClear, _Del_Tool_Insert, _Del_Tool_Where, _Del_Tool_Where, _Del_Tool_Where_Column,_Del_Tool_Update };
        public static List<dynamic> _Dyn_List_databases = new List<dynamic>();
        private static int _listIndex = 0;
        private static string[] _DataSelected_str = new string[] { "DatabaseProvider", "DataBase", "Document", "ConnectionString" };

        //Vetores Volateis
        public static List<dynamic> ListData = new List<dynamic>();
        public static List<dynamic> LisValues = new List<dynamic>();
        public static string SelectCommand(string args)
        {
            for (int i = 0; i < COMMANDS.Length; i++)
            {
                if (COMMANDS[i] == args.ToUpper())
                {
                    _options_call[i]();
                    System.Console.WriteLine(" ");
                    return "Operation " + COMMANDS[i] + " Executed!";
                }
            }
            return "!Not selection!";
        }
        public static void ReadWhereValue()
        {
            //Colocar aqui executar quando localizar campos
            DataModel DataModelReadAll = new DataModel();
            List<dynamic> ListGeneric = new List<dynamic>();
            string value = AnsiConsole.Ask<string>("What's your value you want [green]finder[/]?"); ;
            string field = AnsiConsole.Ask<string>("What's your colum your wish apply  [green]filter[/]?"); ;
            _Dyn_List_databases[0].ReadDocumentWhere(DataModelReadAll, _DataSelected_str[1], _DataSelected_str[2], field, value);
            _ReturnValues(DataModelReadAll, ListGeneric);
        }
        public static void ReadColumnWhereValue()
        {
            DataModel DataModelReadAll = new DataModel();
            List<dynamic> ListGeneric = new List<dynamic>();
            AnsiConsole.Markup("[purple]Enter with Break for stop add Columns[/]");
            while (true)
            {
                string ColumnName = AnsiConsole.Ask<string>("What's your column what your whant [green]return[/]?"); ;
                if (ColumnName.ToUpper() == "BREAK")
                {
                    break;
                }
                ListGeneric.Add(ColumnName);
            }
            string value = AnsiConsole.Ask<string>("What's your value you want [green]finder[/]?"); ;
            string field = AnsiConsole.Ask<string>("What's your colum your wish apply  [green]filter[/]?"); ;
            _Dyn_List_databases[0].ReadDocumentWhere(DataModelReadAll, _DataSelected_str[1], _DataSelected_str[2], field, value);
            _ReturnValues(DataModelReadAll, ListGeneric);

        }
        public static void ReadAllDocument()
        {
            List<dynamic> Values = new List<dynamic>();
            List<dynamic> RowsValues = new List<dynamic>();
            DataModel DataModelReadAll = new DataModel();
            string Values_STR = "";
            if (_listIndex >= 1)
            {
                //Isto é uma conexão de MongoDB
                _Dyn_List_databases[0].ColectAllcolumns(RowsValues, _DataSelected_str[2], _DataSelected_str[1]);
                AnsiConsole.Status()
                .Spinner(Spinner.Known.SimpleDots)
                .Start("Processing your Request...", ctx =>
                 {
                     var table = new Table();
                     table.Alignment(Justify.Center);
                     foreach (var item in RowsValues)
                     {
                         Values_STR = item;
                         Values_STR = Values_STR.Replace('[', ' ').Replace(']', ' ');
                         table.AddColumn(Values_STR).Centered();
                     }

                     _Dyn_List_databases[0].ReadAllDocument(_DataSelected_str[2], _DataSelected_str[1], DataModelReadAll);
                     int RowsCount = 0;
                     int RowLine = 0;
                     while (DataModelReadAll.DataFilterList.Count > RowsCount)
                     {
                         List<Spectre.Console.Rendering.IRenderable> ColumnsSpectre = new List<Spectre.Console.Rendering.IRenderable>();
                         foreach (var itemData in DataModelReadAll.DataFilterList[RowsCount].Values)
                         {
                             while (itemData.Count > RowLine)
                             {
                                 ColumnsSpectre.Add(new Markup("[blue]" + itemData[RowLine] + "[/]"));
                                 RowLine += 1;
                             }
                         }
                         table.AddRow(ColumnsSpectre);
                         RowLine = 0;
                         RowsCount += 1;
                     }

                     Thread.Sleep(2000);
                     AnsiConsole.Render(table);
                     System.Console.WriteLine("Is Result of your database");
                 });
            }
            else
            {
                System.Console.WriteLine("Not have providers created");
            }
        }
        public static void CloseApp()
        {
            System.Console.WriteLine("Thisn application is closed...press any key for close");
            Console.ReadKey();
            Environment.Exit(0);
        }
        public static void CreateNewConnection()
        {
            var data = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
            .Title("Select one [green]provider database[/]?")
            .PageSize(10)
            .AddChoices(new[] {
            "MongoDB", "Firebase",
            "DataBase Local","Use Confing.xml",
                }));
            AnsiConsole.WriteLine($"You Selected: {data} Provaider!");
            string name_CON = AnsiConsole.Ask<string>("What's your connection [green]string[/]?");
            string Data_CON = AnsiConsole.Ask<string>("What's your [green]Database[/]?");
            string Doc_CON = AnsiConsole.Ask<string>("What's your [green]Document[/]?");
            if (data == "MongoDB")
            {
                AnsiConsole.Status()
                .Spinner(Spinner.Known.SimpleDots)
                .Start("Processing your Request...", ctx =>
                 {
                     DbAccess dt = new DbAccess(name_CON, Data_CON, Doc_CON);
                     _Dyn_List_databases.Add(dt);
                     _listIndex += 1;
                     Thread.Sleep(2000);
                     System.Console.WriteLine("Your database is created!");
                     _DataSelected_str[0] = data;
                     _DataSelected_str[1] = Data_CON;
                     _DataSelected_str[2] = Doc_CON;
                     _DataSelected_str[3] = name_CON;
                 });
            }
        }
        public static void Helper()
        {
            var table = new Table();
            table.AddColumn("Command");
            table.AddColumn(new TableColumn("Function").Centered());
            table.Centered();
            table.AddRow("CREATE CON", "[green]Create one new Data Provider for your project[/]");
            table.AddRow("SELECT", "[green]Show your database Documents[/]");
            AnsiConsole.Render(table);
        }
        public static void showConfiguration()
        {
            System.Console.WriteLine(" ");
            var rule = new Rule("[red]Tree data[/]");
            var root = new Tree("[green]Data Provider: " + _DataSelected_str[0] + "[/]");
            var foo = root.AddNode("[yellow]Connection : " + _DataSelected_str[3] + "[/]");
            foo.AddNode("DataBase: " + _DataSelected_str[1]);
            foo.AddNode("Document: " + _DataSelected_str[2]);
            System.Console.WriteLine(" ");
            rule.Alignment = Justify.Center;
            AnsiConsole.Render(rule);
            AnsiConsole.Render(root);
        }
        public static void ModifyDatas()
        {
            var data = AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                        .Title("What field your can [red]modify of your data connection[/]?")
                        .PageSize(10)
                        .AddChoices(new[] {
            "1.DataBase ", "2.Document",
            "3.Connection","Nothing",
                            }));
            if (data != "Nothing")
            {
                string AlterData = AnsiConsole.Ask<string>("New name for [green]" + data.Remove(0, 1) + "[/]?");
                int indexValue = int.Parse(data[0].ToString());
                _DataSelected_str[indexValue] = AlterData;
            }

        }
        private static void updateFields()
        {
            AnsiConsole.WriteLine("Update what collumn and value (old)");
            string Data_CON = AnsiConsole.Ask<string>("What's your [green]Database[/]?");
            string Doc_CON = AnsiConsole.Ask<string>("What's your [green]Document[/]?");
            string Field = AnsiConsole.Ask<string>("What's your [green]What field your wish update[/]?");
            string FieldFind = AnsiConsole.Ask<string>("What's your [green]What field your wish find[/]?");
            string whereValue = AnsiConsole.Ask<string>("What's your [green]What value your wish finder in field[/]?");
            string OldValue = AnsiConsole.Ask<string>("What's your [green]what Old value to wish update[/]?");
            string NewValue = AnsiConsole.Ask<string>("What's your [green]new value[/]?");
            AnsiConsole.WriteLine(_Dyn_List_databases[0].UpdateValueDocument(Data_CON, Doc_CON, Field,FieldFind,whereValue, OldValue, NewValue));
        }
        private static void InsertDocument()
        {

            JsonService JsService = new JsonService();
            ListData = JsService.ReadColluns(ListData, _Dyn_List_databases[0].ReadFristDocument(_DataSelected_str[1], _DataSelected_str[2]));

            foreach (var item in ListData)
            {
                System.Console.WriteLine("What's" + item + "[/] value?");
                string name = Console.ReadLine();
                if (name != "")
                {
                    LisValues.Add(name);
                }
            }

            _Dyn_List_databases[0].InsertValueDocument(_DataSelected_str[1], _DataSelected_str[2], LisValues, ListData);
        }
        private static void ExecuteQuery()
        {
            int x = 0;
            int y = 0;
            //string _QuerySelect="";
            string Query = AnsiConsole.Ask<string>("Digite your [green]Query[/]:");
            List<string> ListQuery = new List<string>();

            while (Query.Length >= x)
            {
                ListQuery.Add(Query[x].ToString());
                x += 1;
                while (Query[x] != ' ')
                {
                    ListQuery[y] += Query[x];
                    x += 1;
                    if (x >= Query.Length)
                    {
                        break;
                    }
                }
                if (ListQuery[y].ToUpper() == "SELECT" || ListQuery[y].ToUpper() == "FROM")
                {
                    ListQuery[y] = ListQuery[y].ToUpper();
                }
                x += 1;
                y += 1;
            }
            //Apartir daqui fazer as operações SELECT * and SELECT <campos> .... (todos eles tem que ter um FROM!)
            if (ListQuery.Contains("INSERT"))
            {
                //Estrutura INSERT + ONDE + CAMPOS + Valores 
                JsonService JsService = new JsonService();
                ListData = JsService.ReadColluns(ListData, _Dyn_List_databases[0].ReadFristDocument(_DataSelected_str[1], _DataSelected_str[2]));
                Console.WriteLine("A quantidade de campos são de : " + ListData.Count);
                x = 1;

                LisValues.Clear();
                while (ListQuery.Count >= x)
                {
                    if (x >= ListQuery.Count)
                    {
                        break;
                    }
                    if (ListQuery[x] == "IN")
                    {
                        ListData.Clear();
                        x += 1;
                        while (ListQuery[x] != "VALUES")
                        {
                            ListData.Add(ListQuery[x]);
                            x += 1;
                        }
                        x += 1;
                        while (ListQuery.Count - 1 > x || ListQuery[x] != "END")
                        {
                            LisValues.Add(ListQuery[x]);
                            x += 1;
                            if (x > ListQuery.Count || ListQuery[x] == "END")
                            {
                                break;
                            }
                        }
                    }
                    //TODO: Lembrar de colocar aqui o comando para fazer a inserção sem colcoar os campos.          
                    x += 1;
                }

                _Dyn_List_databases[0].InsertValueDocument(_DataSelected_str[1], ListQuery[1], LisValues, ListData);
                System.Console.WriteLine("NEW DOCUMENT CREATED!");

            }
            if (ListQuery.Contains("SELECT"))
            {
                System.Console.WriteLine("Fazer um SELECT AQUi");
                //SELECT * ou SELECT <campos>
                x = 0;
                if (ListQuery.Contains("*"))
                {
                    while (ListQuery[x] != "FROM")
                    {
                        x += 1;
                    }
                    _DataSelected_str[2] = ListQuery[x + 1];
                    //Resultado da query
                    if (ListQuery.Contains("WHERE"))
                    {
                        //Colocar aqui condicionais de campo
                        while (ListQuery[x] != "WHERE")
                        {
                            x += 1;
                        }
                        _Dyn_List_databases[0].ReadDocumentWhere(_DataSelected_str[1], _DataSelected_str[2], ListQuery[x + 1], ListQuery[x + 3]);
                    }
                    else
                    {
                        ReadAllDocument();
                        while (ListQuery[x] != "FROM")
                        {
                            x += 1;
                        }
                        //Adicionar aqui a logica para ler somente alguns campos//
                        //Ler os campos existentes e excluir os que o usuario não colocou para listar//
                        //Vou ter que retornar como JS os campos 
                        //_Dyn_List_databases[0].ReadSelectFieldsDocument(_DataSelected_str[1], _DataSelected_str[2], "Campos a serem filtrados");
                    }
                }
            }
        }
        private static void ClearTool()
        {
            Program.Event = "";
        }
        private static void ListTool()
        {
            Stools.ListAllConections(_Dyn_List_databases);
            Program.Event = "true";
            Providers[0] = "LIST PROVIDERS";
            Providers[1] = "PROVIDERS";
            int indexWork = 2;
            for (int i = 0; i < _Dyn_List_databases.Count; i++)
            {
                Providers[indexWork] = _Dyn_List_databases[i]._database_str + "/" + _Dyn_List_databases[i]._document_str;
                indexWork += 1;
                Providers[indexWork] = _Dyn_List_databases[i]._connection_str;
                indexWork += 1;
            }
        }
        private static void _ReturnValues(DataModel DataModelValues, List<dynamic> ListColum)
        {
            List<dynamic> Values = new List<dynamic>();
            List<dynamic> RowsValues = new List<dynamic>();
            DataModel DataModelReadAll = new DataModel();
            string Values_STR = "";
            _Dyn_List_databases[0].ColectAllcolumns(RowsValues, _DataSelected_str[2], _DataSelected_str[1]);
            AnsiConsole.Status()
                .Spinner(Spinner.Known.SimpleDots)
                .Start("Processing your Request...", ctx =>
                 {
                     var table = new Table();
                     table.Alignment(Justify.Center);
                     foreach (var item in ListColum)
                     {
                         Values_STR = item;
                         Values_STR = Values_STR.Replace('[', ' ').Replace(']', ' ');
                         table.AddColumn(Values_STR).Centered();
                     }
                     foreach (var item in RowsValues)
                     {
                         Values_STR = item;
                         Values_STR = Values_STR.Replace('[', ' ').Replace(']', ' ');
                         table.AddColumn(Values_STR).Centered();
                     }

                     int RowsCount = 0;
                     int RowLine = 0;
                     List<Spectre.Console.Rendering.IRenderable> ColumnsSpectre = new List<Spectre.Console.Rendering.IRenderable>();
                     while (DataModelValues.DataFilterList[RowsCount].Values.Count > RowsCount)
                     {
                         while (DataModelValues.DataFilterList.Count > RowLine)
                         {

                             dynamic value = DataModelValues.DataFilterList[RowLine].Values[RowsCount];
                             ColumnsSpectre.Add(new Markup("[blue]" + value + "[/]"));
                             RowLine++;
                         }
                         table.AddRow(ColumnsSpectre);
                         ColumnsSpectre.Clear();
                         RowLine = 0;
                         RowsCount += 1;
                     }

                     Thread.Sleep(2000);
                     AnsiConsole.Render(table);
                     System.Console.WriteLine("Is Result of your database");
                 });
        }

    }

}