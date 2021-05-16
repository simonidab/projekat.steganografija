using System;
using System.IO;
using System.Drawing;

class MainClass {
  //metoda za ubacivanje teksta u sliku
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
          Color pixel = slika.GetPixel(b1, b2);
          R = pixel.R - pixel.R % 2;
          G = pixel.G - pixel.G % 2;
          B = pixel.B - pixel.B % 2;
          slika.SetPixel(b1, b2, Color.FromArgb(R, G, B));

          R = pixel.R + karakter%2;
          karakter /= 2;
          if (j !=5 )
          {
            G = pixel.G + karakter%2;
            karakter /= 2;
            B = pixel.B + karakter%2;
            karakter /= 2;
          }
          slika.SetPixel(b1, b2, Color.FromArgb(R, G, B));
          b2++;
        }
      }
    }
    for(int i = 0;i < 6;i++)
    {
      if(b1 < slika.Height && b2 >= slika.Width)
      {
        b1++;
        b2 = 0;
      }
      if(b2 < slika.Width && b1 < slika.Height)
      {
        Color pixel = slika.GetPixel(b1, b2);
        R = pixel.R - pixel.R % 2;
        G = pixel.G - pixel.G % 2;
        B = pixel.B - pixel.B % 2;
        slika.SetPixel(b1, b2, Color.FromArgb(R, G, B));
        b2++;
      }
    }
    return slika;
  }
  //ove dve pokusaj obrnutog lol
  static int Prazno(int nula)
  {
    if(nula == 0) return 1;
    else return 0;
  }
  /*static string IzSlikeTekst(Bitmap slika)
  {
    int brojac = 0, b1 = 0, b2 = 0;
    string recenica = "";
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
          Color pixel = slika.GetPixel(b1, b2);
          R = pixel.R - pixel.R % 2;
          G = pixel.G - pixel.G % 2;
          B = pixel.B - pixel.B % 2;

          slovo = slovo * 2 + pixel.R % 2;
          brojac += Prazno(slovo);
          Console.WriteLine("{0} slovo r, brojac : {1}", slovo, brojac);
          if (j !=5)
          {
            slovo = slovo * 2 + pixel.G % 2;
            Console.WriteLine("{0} slovo g", slovo);
            slovo = slovo * 2 + pixel.B % 2;
            brojac += Prazno(slovo);
            Console.WriteLine("{0} slovo b", slovo);
          }
          brojac += Prazno(slovo);
          b2++;
        }
      }
      recenica += Convert.ToString((char) Obrnuti(slovo));
      Console.WriteLine("{0} recenica {1}", recenica, Convert.ToInt32("o"));
    }
    return recenica;
  }
  */
  static string IzSlikeTekst1(Bitmap slika)
  {
    short slovo = 0;
    string recenica = String.Empty;
    int brojac = 0;
    for (int i = 0; i < slika.Height; i++)
    {
      for (int j = 0; j < slika.Width; j++)
      {
        brojac = 0;
        slovo = 0;
        Color pixel = slika.GetPixel(j, i);
        for (int n = 0; n < 3; n++)
        {
          slovo = (short)(slovo * 2 + pixel.R % 2);
          brojac++;
          if (brojac!=16)
          {
            slovo = (short)(slovo * 2 + pixel.G % 2);
            slovo = (short)(slovo * 2 + pixel.B % 2);
            brojac+=2;
          }
        }
        if (brojac==16)
        {
          Console.WriteLine(slovo);
          slovo = Obrni(slovo);
          if (slovo == 0)
          {
            return recenica;
          }
          recenica += ((char)slovo).ToString();
        }
      }
    }
    return recenica;
  }

    static short Obrni(short n)
    {
        short obrnuti = 0;
        for (int i = 0; i < 16; i++)
        {
            obrnuti = (short)(obrnuti * 2 + n % 2);
            n = (short)(n/2);
        }
        return obrnuti;
    }
 
  public static void Main (string[] args) {
    string imeUlaza = "drvo.bmp";
    string imeIzlaza = "drvo2.bmp";
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
        Console.WriteLine("Poruka koju ste tražili: {0}", IzSlikeTekst1(slika1));
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