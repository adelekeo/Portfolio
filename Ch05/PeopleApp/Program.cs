
using System;
using Packt.Shared;

Person Delee  = new();
Delee.Name = "Oluwole Adeleke";
Delee.Born= new DateTimeOffset(year:1969, month:10, day:13, hour:  16, minute: 28, second: 0, offset: TimeSpan.FromHours(-5));
WriteLine( format: "{0} was born on {1:D}.",arg0:Delee.Name, arg1: Delee.Born );


Delee.BucketList =  WondersOfTheAncientWorld.HangingGardensOfBabylon | WondersOfTheAncientWorld.MausoleumAtHalicarnassus;
// Delee.BucketList = (WondersOfTheAncientWorl
WriteLine($"{Delee.Name}'s bucket list is  {Delee.BucketList}.");
/*
Delee.FavoriteAncientWonder = WondersOfTheAncientWorld.StatueOfZeusAtOlympia;
WriteLine(
  format: "{0}'s favorite wonder is {1}. Its integer is {2}.",
  arg0:Delee.Name,
  arg1: Delee.FavoriteAncientWonder,
  arg2: (int)Delee.FavoriteAncientWonder);

Person alice = new()
{
  Name = "Alice Jones",
  Born = new(1998, 3, 7, 16, 28, 0,
    // This is an optional offset from UTC time zone.
    TimeSpan.Zero)
};
WriteLine(format: "{0} was born on {1:d}.", // Short date.
  arg0: alice.Name, arg1: alice.Born);
*/