using System;
using UIKit;
using CoreGraphics;
using Foundation;
using enRaya;

namespace tresenraya
{
	public class CustomCollectionSource: UICollectionViewSource
	{
		TresEnRaya juego;

		public CustomCollectionSource()
		{
			juego = new TresEnRaya();
		}

		public override nint NumberOfSections(UICollectionView collectionView)
		{
			return 1;
		}
		public override nint GetItemsCount(UICollectionView collectionView, nint section)
		{
			return 9;
		}
		public override UICollectionViewCell GetCell(UICollectionView collectionView, NSIndexPath indexPath)
		{
			var cell = (CustomCollectionViewCell)collectionView.DequeueReusableCell(CustomCollectionViewCell.CellID, indexPath);
			int index = indexPath.Row;

			cell.UpdateCell(getValorFromBoard(juego.TableroActual.m_Valores[index]));


			return cell;
		}
		string getValorFromBoard(ValorCelda obj)
		{
			string retVal = "";
			switch (obj)
			{
				case ValorCelda.Vacio:
					retVal = "";
					break;
				case ValorCelda.JugadorO:
					retVal = "O";
					break;
				case ValorCelda.JugadorX:
					retVal = "X";
					break;
				default:
					break;
			}
			return retVal;
		}

		[Export("collectionView:didSelectItemAtIndexPath:")]
		public override void ItemSelected(UICollectionView collectionView, NSIndexPath indexPath)
		{
			try
			{
				juego.HacerMovUsuario(indexPath.Row);
				if (juego.TableroActual.JuegoTerminado)
				{
					MessageBox("Juego Terminado", string.Format("Gano el jugador {0}",getValorFromBoard(juego.TableroActual.m_Ganador)));
				}
				collectionView.ReloadData();
			}
			catch (Exception ex)
			{
				MessageBox("Error", string.Format(ex.Message));
			}

		}

		public void MessageBox(string title, string message)
		{
			using (UIAlertView Alerta = new UIAlertView())
			{
				Alerta.Title = title;
				Alerta.Message = message;
				Alerta.AddButton("OK");
				Alerta.Show();
			}
		}
	}
}
