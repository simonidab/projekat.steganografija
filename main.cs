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
  //file header, 14 bajtova
  struct BMFH
  {
    public short bfTip;  //tip slike, u ovom slucaju uvek BM
    public int bfVelicina;  //velicina fajla u bajtovima
    public short bfRezervisano1; //uvek 0
    public short bfRezervisano2; //uvek 0
    public int bfoffBitovi; //broj bajtova od headera do piksela
  }
  //info header
  struct BMIH
  {
    public int biVelicina;  //broj bajtova u headeru
    public int biSirina;  //sirina u pikselima
    public int biVisina;  //visina u pikselima
    public short biRavan; //broj ravni, mora biti 1
    public short biBitCount; //broj bitova u paleti boja
    public int biKompresija; //tip kompresije
    public int biVelicinaSlike;  //velicina slike u bajtovima
    public int biXPikseliPoMetru;  //broj piksela po metru na x osi
    public int biYPikseliPoMetru; //broj piksela po metru na y osi
    public int biBoje;  //broj boja koriscenih
    public int biBitneBoje;  //broj bitnih boja
  }
  //piksel
  struct Pixel
  {
    public int plava;
    public int zelena;
    public int crvena;
  }
  
  static byte Obrnuti(int n)
  {
        int obrnuti = 0;
        for (int i = 0; i < 8; i++)
        {
            obrnuti = obrnuti * 2 + n % 2;
            n /= 2;
        }
        return Convert.ToByte(obrnuti);
  }
  /*static int IzDekadnogUBinarno(int broj)
  {
    int binarno = 0;
    int i = 0;
    do
    {
      binarno += (broj % 2)*Convert.ToInt32(Math.Pow(10, i));
      i++;
      broj /= 2;
    }while(broj > 0);

    return binarno;
  }*/
  public static void Main (string[] args) {
    string imeUlaza = "SlikaZaProveru.bmp";
    string imeIzlaza = "IzlazZaProveru.bmp";
    if (File.Exists(imeUlaza))
    {
        FileStream fs = File.Open(imeUlaza, FileMode.Open);
        BinaryReader ulaz  = new BinaryReader(fs);
        FileStream fs1 = File.Open(imeIzlaza, FileMode.Create);
        BinaryWriter izlaz = new BinaryWriter(fs1);


        BMFH FileHeader;      //unos file headera i prenos u novu sliku
        //for (int i = 0; i < fs.Length / sizeof(int); i++)
        FileHeader.bfTip = ulaz.ReadInt16();
        izlaz.Write(FileHeader.bfTip);
        FileHeader.bfVelicina = ulaz.ReadInt32();
        izlaz.Write(FileHeader.bfVelicina);
        FileHeader.bfRezervisano1 = ulaz.ReadInt16();
        izlaz.Write(FileHeader.bfRezervisano1);
        FileHeader.bfRezervisano2 = ulaz.ReadInt16();
        izlaz.Write(FileHeader.bfRezervisano2);
        FileHeader.bfoffBitovi = ulaz.ReadInt32();
        izlaz.Write(FileHeader.bfoffBitovi);

        BMIH InfoHeader;  //unos info headera i prenos u novu sliku
        InfoHeader.biVelicina =  ulaz.ReadInt32();
        izlaz.Write(InfoHeader.biVelicina);
        InfoHeader.biSirina = ulaz.ReadInt32();
        izlaz.Write(InfoHeader.biSirina);
        InfoHeader.biVisina = ulaz.ReadInt32();
        izlaz.Write(InfoHeader.biVisina);
        InfoHeader.biRavan = ulaz.ReadInt16();
        izlaz.Write(InfoHeader.biRavan);
        InfoHeader.biBitCount = ulaz.ReadInt16();
        izlaz.Write(InfoHeader.biBitCount);
        InfoHeader.biKompresija = ulaz.ReadInt32();
        izlaz.Write(InfoHeader.biKompresija);
        InfoHeader.biVelicinaSlike = ulaz.ReadInt32();
        izlaz.Write(InfoHeader.biVelicinaSlike);
        InfoHeader.biXPikseliPoMetru = ulaz.ReadInt32();
        izlaz.Write(InfoHeader.biXPikseliPoMetru);
        InfoHeader.biYPikseliPoMetru = ulaz.ReadInt32();
        izlaz.Write(InfoHeader.biYPikseliPoMetru);
        InfoHeader.biBoje = ulaz.ReadInt32();
        izlaz.Write(InfoHeader.biBoje);
        InfoHeader.biBitneBoje = ulaz.ReadInt32();
        izlaz.Write(InfoHeader.biBitneBoje);
    
        int velicina = InfoHeader.biSirina*InfoHeader.biVisina*3;

        Pixel pixel;
        Console.WriteLine("Ako zelite da sakrijete poruku u slici napisite 1, a ako zelite da procitate poruku iz slike unesite 2");
        int KojaFunkcija = Convert.ToInt32(Console.ReadLine());
        if (KojaFunkcija==1)
        {
          int brojac = 0;
          //unos teksta
          Console.WriteLine("Unesite text koji zelite da sakrijete u slici: ");
          string tekst = Console.ReadLine();
          for (int i=0;i<tekst.Length;i++)
          {
            short karakter = (short)(tekst[i]); 
            for (int j=0;j<6;j++)
            {
              pixel.plava = ulaz.ReadByte();
              pixel.zelena = ulaz.ReadByte();
              pixel.crvena = ulaz.ReadByte();

              pixel.plava=Obrnuti(pixel.plava);
              pixel.zelena=Obrnuti(pixel.zelena);
              pixel.crvena=Obrnuti(pixel.crvena);

              pixel.plava-=pixel.plava%2;
              pixel.zelena-=pixel.zelena%2;
              pixel.crvena-=pixel.crvena%2;

              pixel.plava+=karakter%2;
              //pixel.plava=Obrnuti(pixel.plava);
              izlaz.Write(Convert.ToByte(pixel.plava));
              karakter/=2;
              if (j!=5)
              {
                pixel.zelena+=karakter%2;
                karakter/=2;
                pixel.crvena+=karakter%2;
                karakter/=2;
              }
              //pixel.zelena=Obrnuti(pixel.zelena);
             // pixel.crvena=Obrnuti(pixel.crvena);
              izlaz.Write(Convert.ToByte(pixel.zelena));
              izlaz.Write(Convert.ToByte(pixel.plava));
            }
          }
          for (int i=0;i<6;i++)
          {
              pixel.plava = ulaz.ReadByte();
              pixel.zelena = ulaz.ReadByte();
              pixel.crvena = ulaz.ReadByte();

              pixel.plava-=pixel.plava%2;
              pixel.zelena-=pixel.zelena%2;
              pixel.crvena-=pixel.crvena%2;

              izlaz.Write(Convert.ToByte(pixel.plava));
              izlaz.Write(Convert.ToByte(pixel.zelena));
              izlaz.Write(Convert.ToByte(pixel.crvena));
          }
          for (int k=0;k<velicina-(tekst.Length*18)-18;k++)
          {
            izlaz.Write(ulaz.ReadByte());
          }
        }
        //citanje poruke
        if (KojaFunkcija==2)
        {
          string ProcitaniTekst = "";
          int karakter2 = 0;
          do
          {
            karakter2=0;
            for (int j=0;j<6;j++)
            {
              pixel.plava = ulaz.ReadByte();
              pixel.zelena = ulaz.ReadByte();
              pixel.crvena = ulaz.ReadByte();
              karakter2 = karakter2*2+pixel.plava%2;
              if(j!=5)
              {
                karakter2 = karakter2*2+pixel.zelena%2;
                karakter2 = karakter2*2+pixel.crvena%2;
              }
            }
            Console.WriteLine(Obrnuti(karakter2));
            ProcitaniTekst+=(char)(Obrnuti(karakter2));
          }while (karakter2!=0);
          Console.WriteLine("Desifrovana poruka je: '{0}'.",ProcitaniTekst);
        }

       
       ulaz.Close();
       izlaz.Close();
    }
    else
        Console.WriteLine( "Slika '{0}' ne postoji", imeUlaza);

  }
}