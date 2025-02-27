using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ENDPOINT_CONFIG
{
    private static string auth = "auth";
    private static string referral = "referral";
    private static string quest = "quest";
    private static string fish = "fish";
    private static string treasure = "treasure";
    private static string leaderboard = "leaderboard";

    public static string PostRequestSignature = auth + "/request-signature";
    public static string PostVerifySignature = auth + "/verify-signature";

    public static string GetRandomTreasure = treasure + "/random";
    public static string PostClaimTreasure(string treasureId) => treasure + $"/claim/{treasureId}";


    public static string GetReferralCode = referral + "/get-code";
    public static string GetReferralList = referral + "/list";

    public static string PostQuestComplete = quest + "/complete";
    public static string GetQuests(int page, int limit) => quest + $"/list?page={page}&limit={limit}";
    public static string PostClaimQuest = quest + "/claim";

    public static string PostFishForFishing = fish + "/fishing";
    public static string PostCatchFish = fish + "/claim";

    public static string GetLeaderBoard(int page, int limit) => leaderboard + $"/list?page={page}&limit={limit}";

    public static string PostDisconnect = auth + "/logout";
}