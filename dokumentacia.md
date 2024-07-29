# Dokumentácia ku projektu CSharpChallenges

## Užívateľská dokumentácia

Aplikácia užívateľa privíta vo webovom prostredí spolu so zoznamom úloh, ktoré môže riešiť. Aby mohol užívateľ nejakú úlohu odovzdať, musí si najprv vytvoriť účet, po prípade, ak už účet vytvorený má, tak sa musí prihlásiť. Vykoná tak pomocou `Login` tlačidla v pravom hornom rohu obrazovky.

Po vytvorení účtu si môže užívateľ už jednoducho vybrať úlohu, ktorú by chcel riešiť. Úlohy si môže vyberať podľa:
* **Názvu**, ktorý často v pár slovách popisuje aj zameranie úlohy
* **Obtiažnosti**, ktorá môže byť troch druhov:
    * *EASY*
    * *MEDIUM*
    * *HARD*

Po vybratí úlohy kliknutím tlačidla `Details` otvoríme zadanie danej úlohy. Každé zadanie úlohy pozostáva z:
* Názvu
* Obtiažnosti
* Textu popisujúcu úlohu a formát vstupných dát
* Príkladu vstupu a správneho výstupu programu

Užívateľ si úlohu rieši u seba vo svojom obľúbenom programátorskom prostredí. Po vyriešení a odladení svojho riešenia môže užívateľ riešenie odovzdať. Odovzdáva sa vždy iba jeden C# súbor, ktorý musí byť korektný. To znamená, že musí obsahovať všetky potrebné `using`y, pretože v opačnom prípade by sa neskompiloval.

Po odovzdaní a kliknutí `Submit Solution` sa riešenie overuje na pripravených vstupných a výstupných dátach, čo môže chvíľu trvať. Výsledkom overenia riešenia môže byť týchto 5 scenárov:
* **Nevalidný súbor**
    * znamená to to, že buď žiaden súbor nebol odovzdaný alebo, že súbor, ktorý užívateľ odovzdal nie je v koretknom formáte `.cs`.
* **Nie je možné skompilovať**
    * ďalšia chybová hláška, ktorá môže nastať je, že odovzdaný súbor nie je možné skompilovať. V takom prípade si treba skontrolovať, či naozaj máte v riešení uvedené všetky potrebné `using`y alebo, či aj vo vašom vývojovom prostredí projekt nehádže nejakú chybu.
* **Chyba pri behu programu**
    * ak by sa náhodou vyskytla behová chyba, ako napríklad zlé indexovanie alebo delenie nulou, tak program vás na to upozorní a vypíše vám aj konkrétnu chybu na ktorej nastal problém.
* **Chybné riešenie**
    * v prípade, že sa program aj skompiluje, aj spustí, ale vráti chybné riešenie, tak užívateľ dostane správu o tom, na akom vstupe program vrátil chybný výsledok. V správe, ktorú dostane je okrem vstupu napísaný aj výstup, ktorý program očakával a rovnako aj chybný výstup, ktorý dostal.
* **Správne riešenie**
    * v prípade, že je riešenie správne užívateľ dostane správu, že všetko úspešne zbehlo a v tabuľke úloh sa mu daná úloha v stĺpci `Solved` zobrazí ako vyriešená.

Po vyriešení úlohy sa môže užívateľ presunúť na ďalšie úlohy a postupne pridávať na obtiažnosti. Užívateľ sa naspäť na tabuľku všetkých úloh dostane kliknutím loga `CSharpChallenges` v ľavom hornom rohu.

## Administrátorská dokumentácia

Súčasťou uživateľskej dokumentácie je aj administrátorská dokumentácia. Účty sú rozdelené na *bežné* a *administrátorské*, čo je zachytené v databázi užívateľov. Administrátorský účet nie je možné vyrobiť cez webovú aplikáciu. Pre úlohy je vytvorený jeden administrátorský účet, cez ktorý je možné vo webovej aplikácií úlohy **pridávať**, **upravovať** a **vymazávať**. Pre edukatívne účely projektu ho tu uvediem:
* *Username:* admin
* *Password*: password

Do administratívneho účtu sa môže admin prihlasiť cez webové rozhranie rovnako ako bežný užívateľ. Po prihlásení sa takisto dostane na tabuľku úloh.

Všetky možnosti, čo môže administrátor robiť si rozpíšeme:

### **Vytváranie nových úloh**

Administrátor môže vytvoriť novú úlohu pomocou tlačidla `Create New Problem` v hornej časti obrazovky. Po stlačení sa mu ukáže formulár, ktorý musí vyplniť. Atribúty, ktoré je potrbné vyplniť sú nasledujúce:
* **Názov**
    * môže byť maximálne 100 znakov dlhý
* **Popis**
    * admin napíše text zadania úlohy. Rovnako je potrebné napísať, v akom formáte budú dáta predávané na vstupe
    * môže byť maximálne 1000 znakov dlhý
* **Obtiažnosť**
    * admin zvolí jednu z obtiažností pre úlohu:
        * *EASY*
        * *MEDIUM*
        * *HARD*
    * pre farebnú reprezentáciu obtiažnosti je potrebné doržiavať dané označenia obtiažností
* **Príklady vsupu**
    * admin sem zapíše príklady vstupov pre danú úlohu. Stránka daný text spracuje ako *Markdown*, ted je dobré používať Markdown syntax pre lepšiu prehľadnosť
    * môže byť maximálne 100 znakov dlhý
* **Príklady výstupov**
    * admin sem zapíše párovo výstupy ku vstupom, ktoré zapísal do políčka `Príklady vstupov`
    * môže byť maximálne 100 znakov dlhý
* **Testové vstupy**
    * tieto vstupy budú tie, na ktorých bude program evaluovaný
    * platí pre nich nasledujúci formát:
        * každý vstup je oddelený pomocou `-----`, čo je 5x `-`
        * na začiatok ani koniec tento oddelovač písať netreba
        * text medzi oddelovačmi je braný celkovo, teda aj s `\n` a `\t` atd.
    * môže byť maximálne 1000 znakov dlhý
* **Testové výstupy**
    * toto políčko bude obsahovať výstupy pre testy napísané v políčku vyššie
    * voči týmto výstupom sa bude porovnávať užívateľové riešenie
    * je dôležité napísať výstupy v **rovnakom poradí** ako boli zapísané párové vstupy
    * môže byť maximálne 1000 znakov dlhý

Takto vytvorená úloha sa zapíše do databázy a zobrazí všetkým užívateľom v zozname úloh. 

### **Upravovanie existujúcich úloh**

Admin môže upravovať aj už existujúcu úlohu. Úlohu si môže vybrať zo zoznamu úloh a kliknutím `Edit` sa dostane do upravovacieho formulára. Ten vyzerá úplne rovnako ako formulár vytvárania novej úlohy len s tým rozdielom, že sú tam vyplnené už aktuálne informácie o úlohe. Admin tak môže opraviť nejaké chybné infomrácie, ak sa tam nejaké nachádzajú, alebo pridať nové testy a výsledky.

Takto upravená úloha sa upraví aj v databáze.

### **Vymazávanie úloh**

Poslednou významnou funkcionalitou administrátorského účtu je úlohy odstraňovať. Admin si zvolí v zozname, ktorú úlohu chce odstrániť a stlačením tlačidla `Delete` úlohu odstráni pre všetkých užívateľov. 

Úloha sa tak odtráni aj z databázy.

## Programátorská dokumentácia

Jedná sa o ASP.NET aplikáciu, v ktorej je použitý architektonický vzor MVC (Model-View-Controller). Popíšeme si jednotlivé prvky zvlášt:

## Model

V projekte sú použité 3 hlavné modely:

### UserModel

UserModel vyzerá nasledujúco:

```cs
public class UserModel
{
    /// <summary>
    /// Gets or sets the unique identifier for the user.
    /// </summary>
    [Required]
    public int UserID { get; set; }

    /// <summary>
    /// Gets or sets the username of the user.
    /// </summary>
    [Required]
    [StringLength(40)]
    [DisplayName("Username:")]
    public string UserName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the password of the user.
    /// </summary>
    [Required]
    [StringLength(40, MinimumLength = 4)]
    [DisplayName("Password:")]
    public string Password { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the email address of the user.
    /// </summary>
    [Required]
    [StringLength(80)]
    [DisplayName("Email:")]
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets a value indicating whether the user has administrative privileges.
    /// </summary>
    [Required]
    public bool Admin { get; set; }
}
```
Tento model si uchováva informácie o používateľovi ako sú:
* *ID*
* *Meno*
* *Heslo*
* *Email* (v projekte sa nijako nepoužíva, ale uchováva sa pre možné budúce rozšírenie)
* *Admin*, čo je `bool`, ktorý hovorí, či je užívateľ admin alebo nie

### ProblemModel

ProblemModel vyzerá nasledujúco:

```cs
public class ProblemModel
{
    /// <summary>
    /// Gets or sets the unique identifier for the problem.
    /// </summary>
    [Required]
    public int ProblemID { get; set; }

    /// <summary>
    /// Gets or sets the title of the problem.
    /// </summary>
    [Required]
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the description of the problem.
    /// </summary>
    [Required]
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the difficulty level of the problem.
    /// </summary>
    [Required]
    public string Difficulty { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets an example test case for the problem.
    /// </summary>
    [Required]
    public string ExampleTestCase { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the solution for the example test case.
    /// </summary>
    [Required]
    public string ExampleTestCaseSolution { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the test cases for the problem.
    /// </summary>
    [Required]
    public string TestCases { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the solutions for the test cases.
    /// </summary>
    [Required]
    public string TestCasesSolution { get; set; } = string.Empty;
}
```

Tento model si uchováva informácie o úlohe. Tieto informácie sú:
* *ID*
* *Názov*
* *Popis*, čo je textový popis problému
* *Obtiažnosť*
* *Príklady vstupov*
* *Príklady výstupov*
* *Testové vstupy*
* *Testové výstupy*

Alternatívny model ku `ProblemModel`u je `ProblemModelWithCheckmark`, ktorý naviac ešte obsahuje informáciu o tom, či aktuálne prihlásený užívateľ má vyriešenú danú úlohu. Tento atribút je reprezentovaný pomocou proprerty `Done`:
```cs
/// <summary>
/// Gets or sets a value indicating whether the problem has been solved by the user.
/// </summary>
[DisplayName("Solved")]
public bool Done { get; set; }
```
a zobrazuje sa v tabuľke úloh ako stĺpec `Solved`.

### UserProblemModel

Tento model prepája `UserModel` a `ProblemModel` a vyzerá nasledujúco:
```cs
public class UserProblemModel
{
    /// <summary>
    /// Gets or sets the unique identifier for the user-problem relationship.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier for the user.
    /// </summary>
    public int UserID { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier for the problem.
    /// </summary>
    public int ProblemID { get; set; }
}
```
Tento model slúži ako model, kde každý vstup reprezentuje, že používateľ s ID `UserID` má vyriešenú úlohu s ID `ProblemID`.

### Databázy

Každému modelu prislúcha aj databáza. Tabuľky pre jednotlivé modely sa postupne volajú:
* `Users`
* `Problems`
* `UserProblem`

Práca z databázami prebieha v súboroch [UsersDAO.cs](CSharpChallenge/CSharpChallenge/Services/UsersDAO.cs), [ProblemDAO.cs](CSharpChallenge/CSharpChallenge/Services/ProblemDAO.cs) a [UserProblemDAO.cs](CSharpChallenge/CSharpChallenge/Services/UserProblemDAO.cs).

Medzi základné funkcie z každého modulu patrí:

### UsersDAO

```cs
public bool FindUserByUserNameAndPassword(UserModel user) {...}
public bool FindUserByUserName(UserModel user) {...}
public bool FindUserByUserNameOrEmail(UserModel user) {...}
public void CreateUser(UserModel user) {...}
public UserModel GetUserByName(string username) {...}
```

### ProblemDAO

```cs
public List<ProblemModel> GetAllProblems() {...}
public List<ProblemModelWithCheckmark> GetAllProblems(UserModel user) {...}
public ProblemModel GetProblemByID(int problemID) {...}
public void CreateProblem(ProblemModel problem) {...}
public void UpdateProblem(ProblemModel problem) {...}
public void DeleteProblemByID(int problemID) {...}
public Tuple<string, string> GetTestCasesByID(int problemID) {...}
public bool IsProblemDoneByUser(int problemID, int userID) {...}
public void SetProblemAsDoneForUser(int problemID, int userID) {...}
```

### UserProblemDAO

```cs
public bool IsProblemDone(int problemID, int userID) {...}
public void SetProblemAsDoneForUser(int problemID, int userID) {...}
```

Tieto funkcie sú teda výlučne na prácu s databázou. Názvy funkcií sú samovysvetľujúce funkcionalitu funkcií. Pre bližšie vysvetlenie funkcií sú v kóde podrobné dokumentačné komentáre.

Databáza, ronako ako aj webová služba, je hostovaná pomocou študentského účtu na *Microsoft Azure*.

## View

View pozostáva z 3 hlavných častí obsahujúcich `.cshtml` formát súborov. 3 hlavné časti sú:

### Home

Home je najäčšia z týchto troch častí. Úvodná stánka `Index.cshtml` obsahuje zoznam úloh, ktoré sú k dispozícií v databáze. Samotný `Index.cshtml` sa zobrazuje iba pre neprihlásených uživateľov. Zoznam úloh pre prihlásaných užívateľov sa zobrazuje pomocou súborov `LoggedInProblemList.cshtml` a `AdminProblemList.cshtml`, pričom prvá stránka zobrazuje úlohy pre bežných uživateľov a druhá pre administrátorov.

V `LoggedInProblemList.cshtml` sa používa ako hlavný model `ProblemModelWithCheckmark`, aby bolo možné vytvoriť stĺpec `Solved` pre daného užívateľa. V `Index.cshtml` aj `AdminProblemList.cshtml` sa používa obyčajný model `ProblemModel`, pretože tu nepotrebujeme informáciu o vyriešení úlohy.

Home ďalej pozostáva z Views ako sú `Create.cshtml`, `Edit.cshtml` a `Details.cshtml`, pričom prvé dva sú formuláre pre adminov na vytváranie nových alebo upravovanie už existujúcich úloh a `Details.cshtml` obsahuje zadanie jednotlivých úloh. 

### Login a Register

Login rovnako ako aj Register obsajujú iba `Index.cshtml` s formulárom na prihlásenie, prípadne na Registráciu nového užívateľa.

## Controller

Controller rovnako ako aj View časť pozostáva z 3 identických častí. To sú:

### HomeController

`HomeController` obsahuje funkcie pre interakciu so všetkými funkciami z Home view. Všetky takéto funkcie majú ako svoju návratovú hodnotu `IActionResult` a tieto funkcie sú:

```cs
public IActionResult Index() {...}
```
ktorá vráti view `Index.cshtml`, ak nie je žiaden užívateľ prihlásený alebo vráti views `LoggedInProblemList.cshtml` alebo `AdminProblemList.cshtml` na základe toho, či je prihlásený užívateľ admin alebo nie.

```cs
public IActionResult Details(int problemID) {...}
```
ktorá načíta problém z databázy podľa ID a vráti jeho view pomocou `Details.cshtml`.

```cs
public IActionResult Create() {...}
```
ktorá vráti `Create.cshtml` view pre vytvorenie novej úlohy.

```cs
public IActionResult CreateProblem(ProblemModel problemModel) {...}
```
ktorá reprezentuje akciu vytvorenia úlohy a zapísania od databázy. Vráti už iba `AdminProblemList.cshtml` aj s vytvorenou úlohou.

```cs
public IActionResult Edit(int problemID) {...}
```
ktorá vráti `Edit.cshtml` view pre upravenie úlohy so zadaným ID.

```cs
public IActionResult EditProblem(ProblemModel problemModel) {...}
```
ktorá reprezentuje akciu upravenia úlohy a zapísania od databázy. Vráti už iba `AdminProblemList.cshtml` aj s upravenou úlohou.

```cs
public IActionResult Delete(int problemID)
```
ktorá reprezentuje akciu vymazania úlohy z databázy.

Hlavnou funkciou v `HomeController`i je funkcia:
```cs
public async Task<IActionResult> SubmitSolution(IFormFile file, int problemId) {...}
```
ktorá v prvom rade skontorluje a asynchrónne nakopíruje odovzdané riešenie užívateľa. Následne skontroluje, či je riešenie možné skompilovať a v prípade že áno, skontroluje odpovede riešenia na testoch. Následne už iba vracia hlášku na základe hodnotenia riešenia. Ako prebieha hodnotenie je v sekcií [Evaluator](#evaluator)

### LoginContoller

`LoginContoller` sa stará o prihlasovanie užívateľov. Obsahuje nasledujúce funkcie:

```cs
public IActionResult Index() {...}
```
ktorá vráti `Index.cshtml` view v Login sekcií, čo je prihlasovací formulár.

```cs
public async Task<IActionResult> Login(UserModel user) {...}
```
ktorá na základe `user`a zistí, či takýto užívateľ existuje v databáze. Ak áno, tak ho prihlási a ak nie, tak vráti príslušnú chybovú hlášku. Prípadné prihlasovanie prebieha asynchrónne.

```cs
public async Task<IActionResult> Logout() {...}
```
ktorá asynchrónne odhlási používateľa a vráti `Index.cshtml` v Login sekcií.

### RegisterContoller

`RegisterController` má na starosti registrovanie nových uživateľov. Obsahuje nasledujúce funkcie:

```cs
public IActionResult Index() {...}
```
ktorá vráti `Index.cshtml` view v Register sekcií, čo je registrovací formulár.

```cs
public async Task<IActionResult> Register(UserModel user) {...}
```
ktorá skontroluje, či užívateľ s rovnakým menom alebo emailom už neexistuje. Ak nie, tak vytvorí nového užívateľa a presmeruje ho na tabuľku úloh a ak áno, tak vráti príslušnú chybovú hlášku.

## Evaluator

Táto sekcia kódu má na starosť testovať korektnosť odovzdaného kódu. Začnime triedou `CSharpFileValidator`:

### CSharpFileValidator

```cs
public class CSharpFileValidator
{
    public bool IsValidCsFile(IFormFile file) {...}

    public bool IsFileAbleToCompile(string filePath) {...}
}
```
Táto trieda obsahuje 2 funkcie. Funkcia `IsValidCsFile` iba kontroluje, či bol odovzdaný nejaký súbor a či jeho prípona je `.cs`. Funkcia `IsFileAbleToCompile` už kontroluje, či odovzdaný súbor je možné skompilovať. Vytvorí si adresár `TempProject/` v ktorom sa pokúsi súbor skompilovať. Ak všetko prebehne bez problémov, tak vráti `True` inak vráti `False`.

### TestCases

```cs
public class TestCases : IEnumerable<TestCases.TestCase>
{
    private readonly int _problemId;
    private List<string> _inputs;
    private List<string> _outputs;
    private readonly ProblemDAO _problemDAO;
    
    public TestCases(ProblemDAO problemDAO, int problemId)
    {
        _inputs = new List<string>();
        _outputs = new List<string>();
        _problemDAO = problemDAO;
        _problemId = problemId;
        LoadTestCasesFromDB();
    }

    private void LoadTestCasesFromDB() {...}

    private void ConvertStringToCases(string stringCases, List<string> casesSeparatedInList) {...}
    
    public struct TestCase
    {
        public string Input { get; }
        public string Output { get; }

        public TestCase(string input, string output)
        {
            Input = input;
            Output = output;
        }
    }

    public IEnumerator<TestCase> GetEnumerator()
    {
        for (int i = 0; i < _inputs.Count; i++)
        {
            yield return new TestCase(_inputs[i], _outputs[i]);
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
```
`TestCases` je trieda, ktorá uchováva v Listoch testové vstupy aj výstupy. Vstupy aj výstupy sa načítavajú pomocou metódy `LoadTestCasesFromDB()` v konštruktore. Po načítaní vstupov a výstupov prekonvertuje `string` na Listy `string`ov reprezentujúce jednotlivé vstupy a výstupy. Ako bolo napísané v [Administrátorskej dokumentácií](#administrátorská-dokumentácia), tak vstupy a výstupy sú od seba rozdelené rozdelovačom: `-----`.

Jeden `TestCase` je reprezentovaný `struct`om `TestCase`, ktorý obsahuje Input a Output. Cez všetky testové vstupy a výstupy sa môžeme jednoducho loopovať, pretože trieda `TestCases` implementuje `interface` `IEnumerable<TestCases.TestCase>` zamerané na `TestCase`. 

### SolutionEvaluator

Posledná trieda na vyhodnocovanie riešenia je trieda `SolutionEvaluator`, ktorá vyzerá nasledovne:

```cs
public class SolutionEvaluator
{
    public Result ProcessSolution(string filePath, int problemId) {...}

    private string? ExecuteCode(string input, string expectedOutput) {...}
}
```

Obe funkcie si prejdeme podrobnejšie:

```cs
 public Result ProcessSolution(string filePath, int problemId)
 {
     TestCases testCases = new TestCases(_problemDAO, problemId);
     bool success = true;
     string? firstErrorMessage = null;

     // Run test cases in parallel
     testCases.AsParallel().ForAll(testCase =>
     {
         string? message = ExecuteCode(testCase.Input, testCase.Output);
         // If an error message is returned, update success flag and store the first error message
         if (message is not null)
         {
             success = false;
             if (firstErrorMessage is null)
             {
                 firstErrorMessage = message;
             }
         }
     });

     return new Result(success, firstErrorMessage);
 }
```
`ProcessSolution` berie ako parameter `filePath` už skopírovaného riešenia od používateľa a `problemId` úlohy, ktorú odovzdal. Z databázy si vytvorí testové vstupy a výstupy pre danú úlohu a uloží ich do `testCases`. Následne spustí paralélne všetky vstupy pomocou paralélneho ForLoopu a čaká, či nenastala nejaká chyba v testoch. Funkcia vracia inštanciu triedy `Restult`, ktorá vyzerá takto:

```cs
public class Result
{
    public readonly bool Success;
   public readonly string? Message;

    public Result(bool success, string? message)
    {
        Success = success;
        Message = message;
    }
}
```
kde `Success` reprezentuje úspech na všetkých testoch a `Message` prípadnú chybu, ktorá nastala. Ak žiadna chyba nenastala, tak vracia `null`.

O beh programu odovzdaného užívateľom sa stará funckia `ExecuteCode`, ktorá vyzerá nasledovne:

```cs
private string? ExecuteCode(string input, string expectedOutput)
{
    string projectDir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "TempProject");

    // Run the compiled DLL using dotnet run
    string dllFilePath = Path.Combine(projectDir, "bin", "Release", "net8.0", "TempProject.dll");

    Process runProcess = new Process();
    runProcess.StartInfo.FileName = "dotnet";
    runProcess.StartInfo.Arguments = $"\"{dllFilePath}\"";
    runProcess.StartInfo.RedirectStandardInput = true;
    runProcess.StartInfo.RedirectStandardOutput = true;
    runProcess.StartInfo.RedirectStandardError = true;
    runProcess.StartInfo.UseShellExecute = false;

    runProcess.Start();

    // Pass input to the program
    using (StreamWriter writer = runProcess.StandardInput)
    {
        writer.WriteLine(input); // Simulate console input
    }

    // Read output from the program
    string output = runProcess.StandardOutput.ReadToEnd().Trim().Replace("\r", "");
    string errors = runProcess.StandardError.ReadToEnd();
    
    runProcess.WaitForExit();

    // If there was an error in program execution, return the error message
    if (!string.IsNullOrEmpty(errors))
    {
        return errors.Split("at")[0];
    }

    // If the output is incorrect, return a detailed error message
    if (expectedOutput != output)
    {
        return $@"Wrong Answer. In test:
        &emsp; {input.Replace("\r\n", "\n").Replace("\n", "\n\t")}
        Expected output:
        &emsp; {expectedOutput.Replace("\r\n", "\n").Replace("\n", "\n\t")}
        But got:
        &emsp; {output.Replace("\r\n", "\n").Replace("\n", "\n\t")}";
    }

    // If the output is correct, return null
    return null;
}
```

Táto funkcia spúšta skompilovaný program na jednotlivých vstupoch daných z parametrov funkcie. Po zbehnutí ďalej kontroluje správnosť výstupov na základe očakávaných výstupov. Ak teda nastala chyba, alebo výsledok, ktorý kód vygeneroval bol chybný, funkcia vráti `string` správu s chybou. Ak bol ale výsledok správny, tak funkcia vracia `null`.

# Prezentácia funkcií

Pri spustení vás privíta stránka zoznamom úloh, ktoré si ako neprihlásený uživateľ môžete prezrieť.

![Index](img/index.png)

Pre prihlásanie alebo registráciu klikneme tlačidlo `Login` v pravom hornom rohu.

## Login

Ak už účet vytvorený máte, tak sa jednoducho pomocou `Username` a `Password` prihlásite.

![Login](img/login.png)

## Register

Ak ale účet ešte nemáte, tak sa registrujete pomocou tlačidla `Register`. Dostanete takýto formulár, ktorý vyplníte:

![Register](img/register.png)

Po prihlásení sa vám ukáže znova zoznam úloh, ktoré je možné riešit, tentokrát ale aj so stĺpcom `Solved`, kde vidíte, či už danú úlohu máte alebo nemáte vyriešenú. Každú úlohu si môžete rozkliknúť pomocou tlačidla `Details`.

![LoggedInUser](img/loggedinuser.png)

Po vybratí ľubovolnej úlohy a jej vyriešení môžete odovzdať jej riešenie pomocou tlačidla na vybratie súboru a kliknutím `Submit Solution`.

![Submit](img/submit.png)

Ukážme si teraz odovzdávanie práve na tejto úlohe. Ak skúsime naprv klinúť `Submit Solution` bez zvoleného súboru alebo iného ako `.cs` súboru dostávame túto hlášku:

![InvalidFile](img/invalidfile.png)

Skúsme teraz odovzdať napríklad tento súbor, ktorý skončí prekladovou chybou, pretože sa príkazom chýba bodkočiarka `;`:
```cs
using System;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World")
        }
    }
}
```

Po odovzdaní dostávame správu `Compilation failed.`

![CompilationFailed](img/compilationfailed.png)

Skúsme teraz odovzdať riešenie, ktoré bude možné skompilovať, ale bude stále vracať výsledok `0 1`:

```cs
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int NumsLength = Int32.Parse(Console.ReadLine());
            List<int> nums = Console.ReadLine().Split(' ').Select(int.Parse).ToList();
            int target = Int32.Parse(Console.ReadLine());

            Console.WriteLine("0 1");
        }
    }
}
```

Po odovzdaní dostávame:

![WrongAnswer](img/wrongAnswer.png)

Ukážme si ešte, ako by vyzerala behová chyba. odovzdajme napríklad tento súbor:

```cs
using System;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int a = 10;
            int b = 0;

            Console.WriteLine(a / b);
        }
    }
}
```
Dostávame takúto behovú chybu:

![RuntimeError](img/runtimeError.png)

Odovzdajme nakoniec správne (naivné) riešenie, ktoré môže vyzerať napríklad takto:

```cs
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int NumsLength = Int32.Parse(Console.ReadLine());
            List<int> nums = Console.ReadLine().Split(' ').Select(int.Parse).ToList();
            int target = Int32.Parse(Console.ReadLine());

            for (int i = 0; i < NumsLength; i++)
            {
                for (int j = i + 1; j < NumsLength; j++)
                {
                    if (nums[i] + nums[j] == target)
                    {
                        Console.WriteLine($"{i} {j}");
                        break;
                    }
                }
            }
        }
    }
}
```

Po odovzdaní dostávame:

![Solved](img/solved.png)

Po vyriešení úlohy sa nám v tabuľke zobrazí `Solved` pri tejto úlohe ako odškrtnutý krížik.

Hocikedy počas pobytu na stránke môže uživateľ stlačit pravo hore `Logout`, čo ho odhlási a presmeruje na prihlasovaciu stránku. Rovnako môže uživateľ hocikedy stlačiť text `CSharpChallenges`, ktorý ho presmeruje na zoznam úloh.

Ukázali sme si, ako sa prihlásiť a vyriešiť úlohy. Ukážme si ešte ako úlohy vytvoriť, upravovať a mazať. Ako bolo vyššie napísané, na tieto funkcie potrebujeme administrátorský účet. Jeden z nich je:
* *Username:* admin 
* *Password:* password

Po prihlásení do administrátorského účtu môžeme vidieť toto:

![Admin](img/admin.png)

Skúsme si vytvoriť problém, ktorý následne aj zmažeme, pre ukázanie všetkých funkcií. Pre vytvorenie problému klikneme `Create New Problem`.

![Create](img/create.png)

Teraz môžeme formulár vyplniť ľubovolne. V tomto príklade vytvorím úlohu, ktorej podstata bude prečítať riadok zo vstupu a vypísať ho trikrát pod seba. Formulár vyplním takto:

Title
```
Print three times
```

Description
```
Read the input and print it three times.
```

Difficulty
```
EASY
```

ExampleTestCase
~~~
```cs
hello


```

---

```cs
world


```
~~~

ExampleTestCaseSolution
~~~
```cs
hello
hello
hello
```

---

```cs
world
world
world
```
~~~

TestCases
~~~
hello
-----
world
~~~

TestCasesSolution
```
hello
hello
hello
-----
world
world
world
```

Úlohu vytvoríme stlačením tlačidla `Create`. Úloha je tak pre všetkých viditeľná a je možné ju vyriešiť. 

Ak by sme ju chceli teraz upraviť, klikli by sme tlačilo `Edit`, ktoré je hneď vedľa úlohy v zozname úloh. Zobrazí sa nám rovanký fomrulár ako pri vytváraní a stlačením `Save` potvrdíme svoje prípadné zmeny.

Ak by sme cheli úlohu vymazať, tak to môžeme urobiť stlačením tlačidla `Delete` pri úlohe v zozname úloh. Úloha sa tak vymaže pre všetkých užívateľov a rovnako aj z dabázy úloh.