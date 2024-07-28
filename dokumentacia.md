# Dokumentácia ku projektu CSharpChallenges

## Užívateľská dokumentácia

Aplikácia užívateľa privíta vo webovom prostredí spolu s tabuľkou úloh, ktoré môže riešiť. Aby mohol užívateľ nejakú úlohu odovzdať, musí si najprv vytvoriť účet, po prípade, ak už účet vytvorený má, tak sa musí prihlásiť. Vykoná tak pomocou `Login` tlačidla v pravom hornom rohu obrazovky.

Po vytvorení účtu si môže užívateľ už jednoducho vybrať úlohu, ktorú by chcel riešiť. Úlohy si môže vyberať podľa:
* **Názvu**, ktorý často v pár slovach popisuje aj zameranie úlohy
* **Obtiažnosti**, ktorá môže byť troch druhov:
    * *EASY*
    * *MEDIUM*
    * *HARD*

Po vybratí úlohy kliknutím tlačidla `Details` otvoríme zadanie danej úlohy. Každé zadanie úlohy pozostáva z:
* Názvu
* Obtiažnosti
* Textu popisujúcu úlohu a formát vstupných dát
* Príklad vstupu a správneho výstupu programu

Užívateľ si úlohu rieši u seba vo svojom obľúbenom programátorskom prostredí. Po vyriešení a odladení svojho riešenia môže užívateľ riešenie odovzdať. Odovzdáva sa vždy iba jeden C# súbor, ktorý musí byť korektný. To znamená, že musí obsahovať všetky potrebné `using`y, pretože v opačnom prípade by sa neskompiloval.

Po odovzdaní a kliknutí `Submit Solution` sa riešenie overuje na pripravených vstupných a výstupných dátach, čo môže chvíľu trvať. Výsledkom overenia riešenia môže byť týchto 5 scenárov:
* **Nevalidný súbor**
    * znamená to to, že buď žiaden súbor nebol odovzdaný alebo, že súbor, ktorú užívateľ odovzdal nie je v koretknom formáte `.cs`.
* **Nie je možné skompilovať**
    * ďalšia chybová hláška, ktorá môže nastať je, že odovzdaný súbor nie je možné skompilovať. V takom prípade si treba skontrolovať, či naozaj máte v riešení uvedené všetky potrebné `using`y alebo, či aj vo vašom vyvojovom prostredí projekt nehádže nejakú chybu.
* **Chyba pri behu programu**
    * ak by sa náhodou vyskytla behová chyba, ako napríklad zlé indexovanie alebo delenie nulou, tak program vás na to upozorní a vypíše vám aj konkrétnu chybu na ktorej nastal problém.
* **Chybné riešenie**
    * v prípade, že sa program aj skompiluje, aj spustí, ale vráti chybné riešenie, tak užívateľ dostane správu o tom, na akom vstupe program vrátil chybný výsledok. V správe, ktorú dostane je okrem vstupu napísaný aj výstup, ktorý program očakával a rovnako aj chybný výstup, ktorý dostal.
* **Správne riešenie**
    * v prípade, že je riešenie správne užívateľ dostane správu, že všetko úspešne zbehlo a v tabuľke úloh sa mu daná úloha v stĺpci `Solved` zobrazí ako vyriešená.

Po vyriešení úlohy sa môže užívateľ presunúť na ďalšie úlohy a postupne pridávať na obtiažnosti. Užívateľ sa naspäť na tabuľku všetkých úloh dostane kliknutím loga `CSharpChallenges` v ľavom hornom rohu.

## Administratívna dokumentácia

Súčasťou uživateľskej dokumentácie je aj administratiívna dokumentácia. Účty sú rozdelené na *bežné* a *administratívne*, čo je zachytené v databázi užívateľov. Administratívny účet nie je možné vyrobiť cez webovú aplikáciu. Pre úlohy je vytvorený jeden administratívny účet, cez ktorý je možné vo webovej aplikácií úlohy **pridávať**, **upravovať** a **vymazávať**. Pre edukatívne účely projektu ho tu uvediem:
* *Username:* admin
* *Password*: password

Do administratívneho účtu sa môže admin prihlasiť cez webové rozhranie rovnako ako bežný užívateľ. Po prihlásení sa takisto dostane na tabuľku úloh.

Všetky možnosti, čo môže administrátor robiť si rozpíšeme:

### **Vytváranie nových úloh**

Administrátor môže vytvoriť novú úlohu pomocou tlačidla `Create New Problem` v hornej časti obrazovky. Po stlačení sa mu ukáže formulár, ktorý musí vyplniť. Atribúty, ktoré je potrbné vyplniť sú nasledujúce:
* **Názov**
    * môže byť maximálne 100 znakov dlhý
* **Popis**
    * admin napíše text zadanie úlohy. Rovnako je potrebné napísať, v akom formáte budú dáta predávané na vstupe
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

Takto vytvorená úloha sa zapíše do databazy a zobrazí všetkým užívateľom v tabuľke. 

### **Upravovanie existujúcich úloh**

Admin môže upravovať aj už existujúcu úlohu. Úlohu si môže vybrať z tabuľky a kliknutím `Edit` sa dostane do upravovacieho formulára. Ten vyzerá úplne rovnako ako formulár vytvárania novej úlohy len s tým rozdielom, že sú tam vyplnené už aktuálne informácie o úlohe. Admin tak môže opraviť nejaké chybné infomrácie, ak sa tam nejaké nachádzajú, alebo pridať nové testy a výsledky.

Takto upravená úloha sa upraví aj v databáze.

### **Vymazávanie úloh**

Poslednou významnou funkcionalitou administratívneho účtu je úlohy odstraňovať. Admin si zvolí v tabuľke, ktorú úlohu chce odstrániť a stlačením tlačidla `Delete` úlohu odstráni pre všetkých užívateľov. 

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

Alternatívny model ku `ProblemModel`u je `ProblemModelWithCheckmark`, ktorý naviac ešte obsahuje informáciu o tom, či aktuálne prihlásený užívateľ má vyriešenú danú úlohu. Tento atribút je reprezentovaný pomocou proprerty:
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

