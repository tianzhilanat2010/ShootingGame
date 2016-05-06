
public enum EventID
{
    HP_Change = 1000,
    Scene_Progress_change = 1001,
    Login_Success = 1002,
    PING = 1003,
    Create_Hp_Bar = 1004,
    Change_Hp_Bar_Postion = 1005,
    Remove_Hp_Bar_Event = 1006,
    CMD_UI_GETUSER_LIST = 1007,
    CMD_UI_PVP_BATTLE_RESULT = 1008,
    CMD_UI_LOAD_PVE_BATTLE_UI = 1009,
    CMD_UI_LOAD_PVP_BATTLE_UI = 1010,
    CMD_UI_PVP_START_CALTIME = 1011,
    CMD_UI_PVP_UPDATE_HP = 1012,
    CMD_UI_PVP_BATTLE_BADGE = 1013,
    CAMERA_Shake = 1014,
    TANK_ChangePos = 1015,
    READY_FILL = 1016,
    START_FILL = 1017,
    FILL_FINSISH = 1018,
    CAMERA_ANGLE_CHANGE = 1019,
    CMD_UI_CREATEMAINTK_LIST = 1020,
    CMD_UI_PLAYERKILL_INFO = 1021,
    CMD_UI_INTEGRAL_INFO = 1022,
    SEND_MAIN_GUN_TYPE = 1023,
    TANK_RESURGENCE = 1024,
    CMD_TANK_DIE        = 1025,
    CMD_TANK_REBORN     = 1026,
    CMD_UI_PVP_END    = 1027,
    CMD_UI_PVE_END    = 1028,
    AIM_POINT_MOVE    = 1029,
    AIMED_AT_TANK     = 1030,
	CHANGE_ZHUNXIN_STATE = 1031,
	CMD_RESUPDATE_FINISHED = 1032,

    UPDATE_DOWNLOAD_FILE_FINISH = 1033,
    UPDATE_VERSION_MESSAGE = 1034,
    UPDATE_FINISH = 1035,
    UPDATE_DOWNLOAD_ERROR = 1036,
    UPDATE_DOWNLOAD_FILELIST_FAIL = 1037,
    UPDATE_DOWNLOAD_FILE = 1038,

    CMD_UI_PVE_NORMAL_END       = 1039,
    CMD_UI_PVE_TASK_END         = 1040,
    CMD_LUA_ZXRADIUS            = 1041,
    CMD_ZX_RADIUS_CHANGE        = 1042,
    CMD_ZX_TANK_FIREHIT         = 1043,
    CMD_ZX_INITZX_DATA          = 1044,
    CMD_LUA_MONITOR_CHANGE      = 1045,
    CMD_ASYNC_UITANK_STATE      = 1046,
    CMD_LUA_ENABLE_MONITOR_RENDER = 1047,

    CMD_LUA_START_GUIDE           = 1048,
    CMD_LUA_NEXT_GUID             = 1049,
    CMD_LUA_END_GUIDE             = 1050,
    CMD_EVENT_SCOUTING            = 1051,
    CMD_EVENT_LIGHTON             = 1052,
    CMD_EVENT_SHOWLOCATION        = 1053,
    CMD_EVENT_DURATIONUPDATE      = 1054,

    CMD_EVENT_TANKPARTBROKEN        = 1055,   
    CMD_EVENT_TANKPARTRECOVER       = 1056,
    CMD_EVENT_TANKINOROUTVIEW     = 1057,//坦克进入/离开视野范围
    Print_Net_Msg = 10008,
}


public enum GuideType 
{
    FirstEnterGame  = 0,
}

public enum GuideState
{
    GuideState_LOCK,
    GuideState_OPEN,
    GuideState_OVER,
}


public enum GuideCondition
{
    GuideCondition_ObjActive   = 1,
    GuideCondition_ExistActive = 2,  // 存在有效
}





