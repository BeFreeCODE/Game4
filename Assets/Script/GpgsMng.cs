using UnityEngine;
using System.Collections;
using UnityEngine.SocialPlatforms;
using GooglePlayGames;
using GooglePlayGames.BasicApi.SavedGame;
using System;
using GooglePlayGames.BasicApi;
public class GpgsMng : MonoBehaviour
{
    void Start()
    {
        PlayGamesPlatform.Activate();
        ConectarGoogle();
    }

    public void ConectarGoogle()
    {
        Social.localUser.Authenticate((bool success) =>
        {
            // handle success or failure
            if (true == success)
            {
                Debug.Log("Login");
            }
            else
            {
                Debug.Log("Login Fail !!");
            }
        });
    }

    public void ShowBoard() //리더보드
    {
        Social.ShowLeaderboardUI();
    }

    public void ShowAchievement()//업적
    {
        Social.ShowAchievementsUI();
    }

    public void ReportScore(int score)
    {
        Social.ReportScore(score, "CgkIm73wv7IFEAIQAQ", success =>
        {
            Debug.Log(success ? "Reported score successfully" : "Failed to report score");
        });
    }

    public void ReportProgress(int score)
    {
        if (score >= 60)
        {
            Social.ReportProgress("CgkIm73wv7IFEAIQAg", 100.0, result =>
            {
                if (result)
                    Debug.Log("Successfully reported achievement progress");
                else
                    Debug.Log("Failed to report achievement");
            });
        }
        if(score >= 100)
        {
            Social.ReportProgress("CgkIm73wv7IFEAIQAw", 100.0, result =>
            {
                if (result)
                    Debug.Log("Successfully reported achievement progress");
                else
                    Debug.Log("Failed to report achievement");
            });
        }
        if (score >= 160)
        {
            Social.ReportProgress("CgkIm73wv7IFEAIQBA", 100.0, result =>
            {
                if (result)
                    Debug.Log("Successfully reported achievement progress");
                else
                    Debug.Log("Failed to report achievement");
            });
        }
        if (score >= 200)
        {
            Social.ReportProgress("CgkIm73wv7IFEAIQBQ", 100.0, result =>
            {
                if (result)
                    Debug.Log("Successfully reported achievement progress");
                else
                    Debug.Log("Failed to report achievement");
            });
        }
        if (score >= 1000)
        {
            Social.ReportProgress("CgkIm73wv7IFEAIQBg", 100.0, result =>
            {
                if (result)
                    Debug.Log("Successfully reported achievement progress");
                else
                    Debug.Log("Failed to report achievement");
            });
        }
    }
}

