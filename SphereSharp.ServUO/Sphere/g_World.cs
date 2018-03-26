using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SphereSharp.ServUO.Sphere._Global;

namespace SphereSharp.ServUO.Sphere
{
    public static class g_World
    {
        public static CObjBasePtr ObjFind(CSphereUID uid)
        {
            if (uid == null)
                return null;

            return SphereSharpRuntime.Current.FindObject(uid);
        }

        public static void Speak( CObjBaseTemplate pSrc, string pszText, HUE_TYPE wHue, TALKMODE_TYPE mode, FONT_TYPE font )

        {
            throw new NotImplementedException();
        // ISINTRESOURCE might be SPKTAB_TYPE ?

            ASSERT(pszText);



            // if ( ISINTRESOURCE(pszText))



            CCharPtr pCharSrc;

            bool fSpeakAsGhost = false;	// I am a ghost ?

	        if (pSrc != null )

	        {

		        //if (pSrc.IsChar())

		        //{

			       // // Are they dead ? Garble the text. unless we have SpiritSpeak

			       // pCharSrc = PTR_CAST(CChar, const_cast<CObjBaseTemplate*>(pSrc));

          //          ASSERT(pCharSrc);

          //          fSpeakAsGhost = pCharSrc->IsSpeakAsGhost();

          //      }

            }

    	    else

	        {

		        //mode = TALKMODE_BROADCAST;

	        }



	        //string sTextUID;

         //   string sTextName; // name labelled text.

         //   string sTextGhost; // ghost speak.



    	    //for (var pClient = g_Serv.GetClientHead(); pClient!=NULL; pClient = pClient->GetNext())

    	    //{

		       // if ( ! pClient.CanHear(pSrc, mode ))

			      //  continue;



    		   // string pszSpeak = pszText;

         //       bool fCanSee = false;

         //       CCharPtr pChar = pClient.GetChar();

		       // if (pChar != NULL )

		       // {

			      //  if (fSpeakAsGhost && ! pChar.CanUnderstandGhost())

			      //  {

				     //   if (sTextGhost.IsEmpty())	// Garble ghost.

				     //   {

					    //    sTextGhost = pszText;

					    //    for (int i=0; i<sTextGhost.GetLength(); i++ )

					    //    {

						   //     if (sTextGhost[i] != ' ' &&  sTextGhost[i] != '\t' )

						   //     {

							  //      sTextGhost.SetAt(i, Calc_GetRandVal(2) ? 'O' : 'o' );

						   //     }

					    //    }

				     //   }

				     //   pszSpeak = sTextGhost;

				     //   pClient->addSound(sm_Sounds_Ghost[Calc_GetRandVal(COUNTOF(sm_Sounds_Ghost))], pSrc );

			      //  }

			      //  fCanSee = pChar.CanSee(pSrc );	// Must label the text.

			      //  if ( ! fCanSee && pSrc )

			      //  {

				     //   if (sTextName.IsEmpty())

				     //   {

					    //    if (pCharSrc && ! pChar.CanDisturb(pCharSrc))

						   //     sTextName.Format( "<System>%s", (LPCTSTR) pszText );

					    //    else

						   //     sTextName.Format( "<%s>%s", (LPCTSTR) pSrc->GetName(), (LPCTSTR) pszText );

				     //   }

				     //   pszSpeak = sTextName;

			      //  }

		       // }



		       // if ( ! fCanSee && pSrc && pClient->IsPrivFlag(PRIV_HEARALL|PRIV_DEBUG ))

		       // {

			      //  if (sTextUID.IsEmpty())

			      //  {

				     //   if (pCharSrc && ! pChar->CanDisturb(pCharSrc))

					    //    sTextUID.Format( "<System [%lx]>%s", pSrc->GetUID(), (LPCTSTR) pszText );

				     //   else

					    //    sTextUID.Format( "<%s [%lx]>%s", (LPCTSTR) pSrc->GetName(), pSrc->GetUID(), (LPCTSTR) pszText );

			      //  }

			      //  pszSpeak = sTextUID;

		       // }



    		   // pClient->addBark(pszSpeak, pSrc, wHue, mode, font );

    	    //}

        }
    }
}
