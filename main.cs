using System;
using System.IO;

class MainClass {
  //file header, 14 bajtova
  struct BMFH
  {
    public int bfTip;  //tip slike, u ovom slucaju uvek BM
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
  
  /*static int CharUBinarno(char a)
  {
    return
  }*/
  public static void Main (string[] args) {
    string imeUlaza = "slika2.bmp";
    string imeIzlaza = "izlaz2.bmp";
    if (File.Exists(imeUlaza))
    {
        FileStream fs = File.Open(imeUlaza, FileMode.Open);
        BinaryReader ulaz  = new BinaryReader(fs);
        FileStream fs1 = File.Open(imeIzlaza, FileMode.Create);
        BinaryWriter izlaz = new BinaryWriter(fs1);
        //unos teksta
        Console.WriteLine("Unesite text koji zelite da sakrijete u slici: ");
        string tekst = Console.ReadLine();

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

        Console.WriteLine("Slika je {0}-bitna.",InfoHeader.biBitCount);
        Pixel pixel;
        //int KojaJeBoja=0;

        for (int i=0;i<tekst.Length;i++)
        {
          short karakter = (short)(tekst[i]); 
          for (int j=0;j<4;j++)
          {
            pixel.plava = ulaz.ReadByte();
            pixel.zelena = ulaz.ReadByte();
            pixel.crvena = ulaz.ReadByte();
            /*Console.WriteLine(ulaz.ReadByte());
            Console.WriteLine(pixel.plava);*/
            pixel.plava-=pixel.plava%2;
            pixel.zelena-=pixel.zelena%2;
            pixel.crvena-=pixel.crvena%2;
          //Console.WriteLine(pixel.plava);
            pixel.plava+=karakter%2;
            izlaz.Write(pixel.plava);
            karakter/=2;
            if (j!=3)
            {
              pixel.zelena+=karakter%2;
              karakter/=2;
              pixel.crvena+=karakter%2;
              karakter/=2;
            }
            izlaz.Write(pixel.zelena);
            izlaz.Write(pixel.crvena);
          }
        }
        Console.Write(FileHeader.bfVelicina);
        Console.Write(velicina);
        for (int k=0;k<FileHeader.bfVelicina-44-(tekst.Length*18);k++)
        {
          izlaz.Write(ulaz.ReadByte());
        }




       izlaz.Write(FileHeader.bfTip);
       ulaz.Close();
       izlaz.Close();
    }
    else
        Console.WriteLine( "Slika '{0}' ne postoji", imeUlaza);

  }
}