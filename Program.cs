using System.Collections;
using System.ComponentModel;
#pragma warning disable CA1416 // beep warning disabler

Console.Clear();

string resposta = "";

int dificult = 0,
rodadas = 0,
etapa = 0,
limite = 8,
cd = 500;

bool gameOver = false;

string[] cachorroTabela = [];
string[] coresDisponivel = {"r","g","b","y"};

Console.WriteLine("Bem vindo ao Genius!!!\nEscolha uma dificuldade entre 1 á 4: ");
resposta = Console.ReadLine()!;

void cores(){
    Console.ForegroundColor = ConsoleColor.White;
    int dice = new Random().Next(1,5)-1;

    if (cachorroTabela.Length > 0){
        int total = cachorroTabela.Length;
        Console.WriteLine(cachorroTabela);
        Console.WriteLine(total);
        cachorroTabela = cachorroTabela.Append(coresDisponivel[dice]).ToArray();
    }
    else{
        cachorroTabela = [coresDisponivel[dice]];
    }

    for (int i = 0; i < cachorroTabela.Length; i++){
        Console.Clear();
        if (cachorroTabela[i] == "r"){
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("∎");
            Console.Beep(1000,cd);

        }
        else if (cachorroTabela[i] == "g"){
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("∎");
            Console.Beep(2000,cd);

        }
        else if (cachorroTabela[i] == "b"){
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("∎");
            Console.Beep(3000,cd);

        }
        else if (cachorroTabela[i] == "y"){
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("∎");
            Console.Beep(4000,cd);

        }
    }
}

void memory(){
    ConsoleKeyInfo tecla = Console.ReadKey();
    string teclaPressionada = tecla.KeyChar.ToString().ToLower().Trim();

    if (teclaPressionada == cachorroTabela[etapa]){
        etapa++;
        if (etapa < cachorroTabela.Length){
            memory();
        }
    }
    else if (teclaPressionada == "r"
    || teclaPressionada == "g"
    || teclaPressionada == "b"
    || teclaPressionada == "y"
    || tecla.Key == ConsoleKey.Escape){
        gameOver = true;
    }
    else{
        memory();
    }
}

void loop(){
    rodadas++;
    
    cores();

    Console.Clear();
    Console.ResetColor();
    Console.Write($"Rodada: ");
    Console.ForegroundColor = ConsoleColor.Gray;
    Console.BackgroundColor = ConsoleColor.White;
    Console.WriteLine(rodadas);

    Console.ResetColor();
    Console.WriteLine("As cores e seus respectivos sons: ");
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine("[R] para Vermelho");
    Console.Beep(1000,cd/2);
    Console.ForegroundColor = ConsoleColor.Blue;
    Console.WriteLine("[B] para Azul");
    Console.Beep(2000,cd/2);
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("[G] para Verde");
    Console.Beep(3000,cd/2);
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine("[Y] para Amarelo");
    Console.Beep(4000,cd/2);
    Console.ForegroundColor = ConsoleColor.White;
    Console.WriteLine("Digite a sequência demonstrada: ");

    etapa = 0;
    memory();

    if (gameOver == true){
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("\nGame Over");
    }
    else if (rodadas < limite){
        loop();
    }
    else{
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("\nParabéns mané");
    }
}

if (int.TryParse(resposta, out dificult)){
    dificult = int.Parse(resposta);
    if (dificult > 0 && dificult <= 4){
        switch (dificult)
        {
            case 1:
                limite = 8;
            break;
            case 2:
                limite = 14;
            break;
            case 3:
                limite = 20;
            break;
            default:
                limite = 31;
            break;
        }
        Console.WriteLine("Parabens por saber escrever um numero :D\nPressione qualquer tecla para continuar!");
        Console.ReadKey();
        loop();
    }
    else{
        Console.WriteLine("tu sabe que é 1 até 4 certo?");
    }
}
else{
    Console.WriteLine("Cade a poura do numero >:(((((");
}
Console.ResetColor();