SELECT datediff(ss, min(gba.entry_time), max(gba.entry_time))
FROM g_batch_audit gba
WHERE batch_run_num = {Param1}
AND task_id = {Param2}