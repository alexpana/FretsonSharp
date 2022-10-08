using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace Fretson.Core
{
    public class Chord
    {
        public int FirstFret;

        private int Id;

        public string Shape;

        public List<List<ChordString>> Strings;

        public static Chord For(Note note, string name)
        {
            var chordIndex = 0;
            for (var i = 0; i < Constants.ChordNames.Length; ++i)
                if (Constants.ChordNames[i].Equals(name))
                {
                    chordIndex = i;
                    break;
                }

            var textAsset = Resources.Load<TextAsset>($"Chords/chord_{(int)note}_{chordIndex}");
            return JsonConvert.DeserializeObject<JsonChord>(textAsset.text).results[0];
        }

        public override string ToString()
        {
            return $"{nameof(Id)}: {Id}, {nameof(Strings)}: {Strings}, {nameof(Shape)}: {Shape}";
        }

        public struct ChordString
        {
            public int FretNo;
            public bool IsRootNote;
            public string Symbol;
            public bool IsMuted;

            public override string ToString()
            {
                return
                    $"{nameof(FretNo)}: {FretNo}, {nameof(IsRootNote)}: {IsRootNote}, {nameof(Symbol)}: {Symbol}, {nameof(IsMuted)}: {IsMuted}";
            }
        }

        private class JsonChord
        {
            public List<Chord> results;
        }
    }
}