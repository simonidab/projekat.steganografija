using System;
using System.IO;
using System.Drawing;

class MainClass {
  public static Bitmap TekstUSliku(string tekst, Bitmap slika)
  {
    int b1 = 0, b2 = 0, R = 0, G = 0, B = 0;
    for (int i=0;i<tekst.Length;i++)
    {
      short karakter = (short)(tekst[i]); 
      for (int j=0;j<6;j++)
      {
        if(b1 < slika.Height && b2 >= slika.Width)
        {
          b1++;
          b2 = 0;
        }
        if(b2 < slika.Width && b1 < slika.Height)
        {
          Color pixel = slika.GetPixel(b2, b1);
          R = pixel.R - pixel.R % 2;
          G = pixel.G - pixel.G % 2;
          B = pixel.B - pixel.B % 2;
          slika.SetPixel(b2, b1, Color.FromArgb(R, G, B));

          R = pixel.R + karakter%2;
          karakter /= 2;
          if (j !=5 )
          {
            G = pixel.G + karakter%2;
            karakter /= 2;
            B = pixel.B + karakter%2;
            karakter /= 2;
          }
          slika.SetPixel(b2, b1, Color.FromArgb(R, G, B));
          b2++;
        }
      }
    }
    return slika;
  }
   static int Prazno(int nula)
  {
    if(nula == 0) return 1;
    else return 0;
  }
  static string IzSlikeTekst(Bitmap slika)
  {
    int brojac = 0, b1 = 0, b2 = 0;
    string recenica = String.Empty;
    int slovo = 0;
    while(brojac < 17)
    {
      brojac = 0;
      for (int j=0;j<6;j++)
      {
        if(b1 < slika.Height && b2 >= slika.Width)
        {
          b1++;
          b2 = 0;
        }
        if(b2 < slika.Width && b1 < slika.Height)
        {
          Color pixel = slika.GetPixel(b2, b1);

          slovo = slovo * 2 + pixel.R % 2;
          brojac += Prazno(slovo);
          if (j !=5)
          {
            slovo = slovo * 2 + pixel.G % 2;
            slovo = slovo * 2 + pixel.B % 2;
            brojac += Prazno(slovo);
          }
          brojac += Prazno(slovo);
          b2++;
        }
      }
      recenica += Convert.ToString((char) Obrni(slovo));
      slovo=0;
    }
    return recenica;
  }
  static int Obrni(int n)
    {
        int obrnuti = 0;
        for (int i = 0; i < 16; i++)
        {
            obrnuti = obrnuti * 2 + n % 2;
            n/=2;
        }
        return obrnuti;
    }
 
  public static void Main (string[] args) {
    string imeUlaza = "slika1.bmp";
    string imeIzlaza = "IzlazZaProveru.bmp";
    int izbor = 0;
    Console.WriteLine("Dobar dan i dobrodosli tajnoviti ljudi !!");
    Console.WriteLine();
    Console.WriteLine("Među opcijama imamo da:");
    Console.WriteLine();
    do
    {
      Console.WriteLine("Ako želite da sakrijete poruku u slici napisite 0, ako želite da procitate poruku iz slike unesite 1, a ako želite da prekinete program unesite 2");
      Console.Write("Odgovor: ");
      string odgovor=Console.ReadLine();
      while (!Int32.TryParse(odgovor, out izbor))
      {
        Console.WriteLine("__________________________________________________________________");
        Console.WriteLine();
        Console.WriteLine("Niste pravilno uneli broj funkcije koju želite da izvršite, molimo vas unesite broj ponovo.");
        Console.WriteLine();
        Console.WriteLine("Ako želite da sakrijete poruku u slici napisite 0, ako želite da procitate poruku iz slike unesite 1, a ako želite da prekinete program unesite 2");
        Console.Write("Odgovor: ");
        odgovor = Console.ReadLine();
      }
      if(izbor == 0)
      {
        Console.WriteLine("__________________________________________________________________");
        Console.WriteLine();
        Bitmap slika = new Bitmap(imeUlaza);
        Console.Write("Unesite tekst koji želite da sakrijete u slici: ");
        string tekst = Console.ReadLine();
        slika = TekstUSliku(tekst, slika);
        slika.Save(imeIzlaza);
        Console.WriteLine();
        Console.WriteLine("Uspešno sakrivena poruka!");
      }
      else if(izbor == 1)
      {
        //pitanje kad vidis elemejo ali jel se ukucava ime fajla slike? kao readline da ispravim posle
        //mislim da je dobro ovako
        Bitmap slika1 = new Bitmap(imeIzlaza);
        Console.WriteLine("__________________________________________________________________");
        Console.WriteLine();
        Console.WriteLine("Poruka koju ste tražili: {0}", IzSlikeTekst(slika1));
        Console.WriteLine();
      }
      else if(izbor != 2 && izbor != 1 && izbor != 0)
      {
         Console.WriteLine("__________________________________________________________________");
        Console.WriteLine();
        Console.WriteLine("........");
        Console.WriteLine("Evo ponovo:");
        Console.WriteLine();
      }
      Console.WriteLine("__________________________________________________________________");
      Console.WriteLine();
      if(izbor == 2) 
      {
        Console.WriteLine("Doviđenja!");
        Console.WriteLine("__________________________________________________________________");
      }
    }while(izbor != 2);
    

  }
}