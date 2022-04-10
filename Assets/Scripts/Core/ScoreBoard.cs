using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Realtime;

public class ScoreBoard : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private CanvasGroup canvasGroup = null;

    [SerializeField]
    private Transform contentTransform = null;

    [SerializeField]
    private GameObject playerInfoCellPrefab = null;

    private List<ScoreBoardPlayerInfoCell> scoreBoardPlayerInfoCells = new List<ScoreBoardPlayerInfoCell>();

    Player[] players = default;

    private void Start()
    {
        players = PhotonNetwork.PlayerList;

        for(int i = 0; i < players.Length; i++)
        {
            GameObject cells = Instantiate(playerInfoCellPrefab, contentTransform);

            ScoreBoardPlayerInfoCell scoreBoardPlayerInfoCell = cells.GetComponent<ScoreBoardPlayerInfoCell>();

            scoreBoardPlayerInfoCells.Add(scoreBoardPlayerInfoCell);

            scoreBoardPlayerInfoCells[i].PlayerNameTxt.text = players[i].NickName.ToString();
            scoreBoardPlayerInfoCells[i].PlayerPointsTxt.text = "0";
        }
    }

    public void OpenMenu()
    {
        canvasGroup.alpha = 1;

        for (int i = 0; i < players.Length; i++)
        {
            scoreBoardPlayerInfoCells[i].PlayerNameTxt.text = players[i].NickName.ToString();
            if (players[i].CustomProperties.ContainsKey("Points"))
                scoreBoardPlayerInfoCells[i].PlayerPointsTxt.text = ((int)players[i].CustomProperties["Points"]).ToString();
        }
    }

    public void CloseMenu()
    {
        canvasGroup.alpha = 0;
    }
}