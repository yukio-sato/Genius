#pragma warning disable CA1416 // beep warning disabler

using System.Dynamic;

Console.Clear(); // clear console

string dificultName = ""; // selected dificult name

ConsoleColor dificultColor = ConsoleColor.White; // selected dificult color

int dificult = 0, // dificult number
rodadas = 0, // total of rounds done
etapa = 0, // where you are in order
limite = 8, // limit to loop the game
cd = 500, // cooldown for sounds
antiCd = 1; // multiplier to make cd to 0 if needed

bool gameOver = false; // condition to make you LOOSE!

string[] orderMemory = []; // bot memory order!
string[] coresDisponivel = ["r","g","b","y"]; // color enabled
string[] colorName = [
    "Vermelho",
    "Verde",
    "Azul",
    "Amarelo"
    ]; // color enabled

ConsoleColor[] colorColored = [
    ConsoleColor.Red,
    ConsoleColor.Green,
    ConsoleColor.Blue,
    ConsoleColor.Yellow
    ]; // color enabled

dificulting(); // start question dificult
void dificulting(){
    Console.Clear(); // clear console
    Console.ForegroundColor = ConsoleColor.White;
    Console.WriteLine("Bem vindo ao Genius!!!"); // just a welcome phrase
    Console.WriteLine("Escolha uma dificuldade: ");
    for (int i = 1; i <= 4; i++){ // show all enabled dificults
        dificult = i;
        dificultSelect();
        Console.ForegroundColor = dificultColor; // set text color to dificult showed color
        Console.WriteLine($"[{i}] {dificultName}"); // show dificult number and name
    }
    Console.ForegroundColor = ConsoleColor.White;
    ConsoleKeyInfo tecla = Console.ReadKey(); // get you keybind pressed
    Console.WriteLine(); // design
    string resposta = tecla.KeyChar.ToString().ToLower().Trim(); // remove spaces, make it lower and transform into string
    if (int.TryParse(resposta, out dificult)){ // check if possible to convert the string to int
        dificult = int.Parse(resposta); // convert string to int
        if (dificult > 0 && dificult <= 4){ // if are in between 1 and 4
            dificultSelect();
            Console.WriteLine("Pressione qualquer tecla para continuar. . .");
            Console.ReadKey(); // nothing just visual
            loop(); // start the game!
        }
        else{
        dificulting(); // start question dificult
        }
    }
    else{
    dificulting(); // start question dificult
    }
}

void dificultSelect(){
    switch (dificult){ // dificult set
        case 1: // easy
            limite = 8;
            dificultName = "Fácil";
            dificultColor = ConsoleColor.Green;
        break;
        case 2: // medium
            limite = 14;
            dificultName = "Médio";
            dificultColor = ConsoleColor.Yellow;
        break;
        case 3: // hard
            limite = 20;
            dificultName = "Díficil";
            dificultColor = ConsoleColor.Red;
        break;
        default: // nightmare
            limite = 31;
            dificultName = "Pesadelo";
            dificultColor = ConsoleColor.Magenta;
        break;
    }
}

void cores(){
    Console.ForegroundColor = ConsoleColor.White;
    int dice = new Random().Next(1,5)-1; // randomize the color with number

    int total = orderMemory.Length; // total from table "orderMemory"
    orderMemory = orderMemory.Append(coresDisponivel[dice]).ToArray(); // push a new value (in this case the random color)

    for (int i = 0; i < orderMemory.Length; i++){
        Console.Clear(); // avoid color history showed
        for (int i2 = 0; i2 < coresDisponivel.Length; i2++){
            if (orderMemory[i] == coresDisponivel[i2]){
                Console.ForegroundColor = colorColored[i2];
                Console.WriteLine("╔══════╗\n║██████║\n║██████║\n║██████║\n╚══════╝");
                Console.Beep(1000*(i2+1),cd);
            }
        }
    }
}

void memory(){
    ConsoleKeyInfo tecla = Console.ReadKey(); // get you keybind pressed
    string teclaPressionada = tecla.KeyChar.ToString().ToLower().Trim(); // remove spaces, make it lower and transform into string
    if (dificult > 2){
        antiCd = 0; // dont play animation for colors and sounds
        status(); // start the function "status"
    }
    else if (dificult == 2 && etapa >= (orderMemory.Length/2)){
        antiCd = 0; // dont play animation for colors and sounds
        status(); // start the function "status"
    }
    bool inRules = false; // value to see if you are in rule
    for (int i = 0; i < coresDisponivel.Length; i++){
        if (coresDisponivel[i] == teclaPressionada){ // check if you pressed one of the colors
            inRules = true; // you are in rules
        }
    }

    if (teclaPressionada == orderMemory[etapa]){ // if pressed correctly
        etapa++; // increase you step in order
        if (etapa < orderMemory.Length){ // check if you has colors
            memory(); // start again with a new color
        }
    }
    else if (inRules == true || tecla.Key == ConsoleKey.Escape){ // if you pressed wrong color or used "ESC" to leave
        gameOver = true; // You LOOSE!
    }
    else{
        antiCd = 0; // dont play animation for colors and sounds
        status(); // start the function "status"
        memory(); // start again with a new color
    }
}

void status(){
    Console.Clear(); // clear the console to avoid color history
    Console.ResetColor(); // remove color
    Console.Write($"Rodada atual: ");
    Console.ForegroundColor = ConsoleColor.Black;
    Console.BackgroundColor = ConsoleColor.White;
    Console.WriteLine("「"+rodadas+"」"); // show the rounds that you are

    Console.ResetColor(); // remove color
    Console.WriteLine("As cores e seus respectivos sons: ");
    for (int i = 0; i < coresDisponivel.Length; i++){
        Console.ForegroundColor = colorColored[i];
        Console.WriteLine($"「{coresDisponivel[i].ToUpper()}」 para {colorName[i]}");
        if (antiCd > 0){Console.Beep(1000*(i+1),cd/2);}
    }
    Console.ForegroundColor = ConsoleColor.White;
    Console.WriteLine("Digite a sequência demonstrada: "); // ask for you type the sequence showed
    antiCd = 1; // back to normal
}

void loop(){
    rodadas++; // increase your rounds done
    
    cores(); // show colers partterns

    status(); // show the round that you are, color

    etapa = 0; // the step that you are in order color
    memory(); // start to "memory" function

    if (gameOver == true){ // when finished "memory" see if you LOOSE!
        string original = "";
        for (int i = 0; i < orderMemory.Length; i++){
            original += orderMemory[i];
        }
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"\nOrdem Correta: {original}"); // you LOOSE!
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Game Over"); // you LOOSE!
    }
    else if (rodadas < limite){ // if you didnt finished the limit, start from zero
        loop(); // "loop" function :)
    }
    else{
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write("\nParabéns, você completou no modo: 「");
        Console.ForegroundColor = dificultColor; // color of dificult selected
        Console.Write(dificultName); // the dificult that you selected
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("」!");
    }
}
Console.ResetColor(); // set color to original before starting this program.