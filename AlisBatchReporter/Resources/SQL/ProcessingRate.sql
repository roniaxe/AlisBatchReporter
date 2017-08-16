SELECT
   BATCH_RUN_NUM,
   BATCH_NAME,
   BATCH_ID,
   TASK_NAME,
   TASK_ID,
   MIN(ENTRY_TIME) START,
   MAX(ENTRY_TIME) FINISH,
   COUNT(*) LINES,
   COUNT(DISTINCT 
   CASE
      WHEN
         CHUNK_ID >= 0 
      THEN
         CHUNK_ID 
      ELSE
         0 
   END
) CHUNKS, SUM(PROCESSED) PROCESSED, SUM(ERRORS) ERRORS, CAST(DATEDIFF(S, MIN(ENTRY_TIME), MAX(ENTRY_TIME)) / 3600. AS DECIMAL(10, 2)) HOURS , CAST(DATEDIFF(S, MIN(ENTRY_TIME), MAX(ENTRY_TIME)) / 60. AS DECIMAL(10, 2)) MINS , DATEDIFF(S, MIN(ENTRY_TIME), MAX(ENTRY_TIME)) SECONDS 
FROM
   (
      SELECT
         BATCH_RUN_NUM,
         BATCH_NAME,
         G.BATCH_ID,
         TASK_NAME,
         G.TASK_ID,
         CHUNK_FLAG,
         ENTRY_TIME,
         CHUNK_ID,
         CASE
            WHEN
               DESCRIPTION LIKE '%completed , reached%' 
            THEN
               1 
            ELSE
               0 
         END
         PROCESSED, 
         CASE
            WHEN
               ENTRY_TYPE = 6 
            THEN
               1 
            ELSE
               0 
         END
         ERRORS 
      FROM
         G_BATCH_AUDIT G 
         LEFT OUTER JOIN
            T_TASK T 
            ON G.TASK_ID = T.TASK_ID 
         JOIN
            T_BATCH B 
            ON G.BATCH_ID = B.BATCH_ID 
      WHERE
         G.ENTRY_TIME > {Param1}
         AND G.ENTRY_TIME - 1 <= {Param2}
		 AND G.TASK_ID != 0
      UNION ALL
      SELECT
         BATCH_RUN_NUM,
         BATCH_NAME,
         G.BATCH_ID,
         SUBSTRING(DESCRIPTION, 7, CHARINDEX('''', SUBSTRING(DESCRIPTION, 7, 100)) - 1) TASK_NAME,
         T.TASK_ID,
         CHUNK_FLAG,
         ENTRY_TIME,
         CHUNK_ID,
         CASE
            WHEN
               DESCRIPTION LIKE '%completed , reached%' 
            THEN
               1 
            ELSE
               0 
         END
         PROCESSED, 
         CASE
            WHEN
               ENTRY_TYPE = 6 
            THEN
               1 
            ELSE
               0 
         END
         ERRORS 
      FROM
         G_BATCH_AUDIT G 
         JOIN
            T_BATCH B 
            ON G.BATCH_ID = B.BATCH_ID , T_TASK T 
      WHERE
         1 = 1 
         AND G.ENTRY_TIME > {Param1}
         AND G.ENTRY_TIME - 1 <= {Param2}
         AND G.TASK_ID = 0 
         AND DESCRIPTION LIKE 'Task ''%' 
         AND SUBSTRING(DESCRIPTION, 7, CHARINDEX('''', SUBSTRING(DESCRIPTION, 7, 100)) - 1) = T.TASK_NAME 
   )
   X 
GROUP BY
   BATCH_RUN_NUM, TASK_NAME, BATCH_NAME, BATCH_ID, TASK_ID, CHUNK_FLAG 
ORDER BY
   MIN(ENTRY_TIME)