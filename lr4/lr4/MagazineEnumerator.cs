﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lr4
{
    class MagazineEnumerator : IEnumerator
    {

        Magazine magazine;
        object CurrenaArticle;
        int ind = 0;
        public MagazineEnumerator(Magazine a)
        {
            magazine = a;
        }
        public object Current
        {
            get
            {
                return CurrenaArticle;
            }
        }

        public bool MoveNext()
        {
            ind++;
            for (int i = ind; i < magazine.Articles.Count; i++)
            {
                Article article = (Article)magazine.Articles[i];
                Person author = article.Author;
                if (magazine.Editors.LastIndexOf(author) == -1)
                {
                    CurrenaArticle = magazine.Articles[i];
                    break;
                }
            }
            return (ind < magazine.Articles.Count);

        }

        public void Reset()
        {
            ind = 0;
        }
    }
}