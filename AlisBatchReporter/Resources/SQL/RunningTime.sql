SELECT datediff(ss, min(gba.entry_time), max(gba.entry_time))
FROM g_batch_audit gba
WHERE batch_run_num = {batchRunNum}
AND task_id = {taskId}