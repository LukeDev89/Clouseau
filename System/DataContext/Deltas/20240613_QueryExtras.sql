CREATE FUNCTION fn_ObtenerDiasHabilesInicioFin (
    @FechaInicio DATE,
    @FechaFin DATE
)
RETURNS TABLE
AS
RETURN (
    WITH Fechas AS (
        SELECT @FechaInicio AS Fecha  -- Comienza con la fecha de inicio
        UNION ALL
        SELECT DATEADD(DAY, 1, Fecha)
        FROM Fechas
        WHERE DATEADD(DAY, 1, Fecha) <= @FechaFin  -- Continúa hasta la fecha de fin
    )
    SELECT Fecha
    FROM Fechas
    WHERE (DATEPART(WEEKDAY, Fecha) + @@DATEFIRST - 1) % 7 + 1 BETWEEN 2 AND 6  -- Ajuste para cualquier configuración de DATEFIRST, lunes a viernes
);

GO

CREATE PROCEDURE [dbo].[usp_GetHorasExtras]
    @selectedMonth INT
AS
BEGIN
    DECLARE @FechaActual DATE;
    IF @selectedMonth = MONTH(GETDATE())
        SET @FechaActual = GETDATE();
    ELSE
        SET @FechaActual = EOMONTH(GETDATE(), -1);  -- Calcula el último día del mes anterior

    DECLARE @InicioPeriodo DATE = DATEADD(MONTH, DATEDIFF(MONTH, 0, @FechaActual) - 1, 15);  -- 16 del mes anterior
    DECLARE @FinPeriodo DATE = DATEADD(DAY, 14, @InicioPeriodo);  -- 15 del mes corriente

    -- Horas extras incurridas en días hábiles (horas cargadas - 8).
    SELECT 
        (RTRIM(u.lastname) + ', ' + RTRIM(u.firstname)) AS Integrante,
        CONVERT(DATE, dh.Fecha) AS Fecha,
        COALESCE(SUM(a.hours) - 8, 0) AS HorasExtras,  -- Calcula horas extras
        CASE 
            WHEN COALESCE(SUM(a.hours), 0) > 8 THEN 'Horas extra'
            ELSE 'Horas normales'
        END AS Estado
    FROM 
        [Clouseau].[dbo].[Users] u 
    CROSS JOIN 
        dbo.fn_ObtenerDiasHabilesInicioFin(@InicioPeriodo, @FinPeriodo) AS dh
    LEFT JOIN 
        [Clouseau].[dbo].[TaskProgress] a ON a.userid = u.id AND CAST(a.date AS DATE) = dh.Fecha
    GROUP BY 
        u.lastname, u.firstname, dh.Fecha
    HAVING 
        COALESCE(SUM(a.hours), 0) > 8

    UNION ALL

    -- Horas incurridas en días no hábiles (total horas).
    SELECT 
        (RTRIM(u.lastname) + ', ' + RTRIM(u.firstname)) AS Integrante,
        CAST(a.date AS DATE) AS Fecha,
        COALESCE(SUM(a.hours), 0) AS HorasCargadas,  -- Total de horas en días no hábiles
        'Trabajo en día no hábil' AS Estado
    FROM 
        [Clouseau].[dbo].[Users] u 
    LEFT JOIN 
        [Clouseau].[dbo].[TaskProgress] a ON a.userid = u.id
    WHERE 
        CAST(a.date AS DATE) BETWEEN @InicioPeriodo AND @FinPeriodo AND 
        CAST(a.date AS DATE) NOT IN (SELECT Fecha FROM dbo.fn_ObtenerDiasHabilesInicioFin(@InicioPeriodo, @FinPeriodo))
    GROUP BY 
        u.lastname, u.firstname, CAST(a.date AS DATE)
    HAVING 
        COALESCE(SUM(a.hours), 0) > 0

    ORDER BY 
        Integrante, Fecha;
END;