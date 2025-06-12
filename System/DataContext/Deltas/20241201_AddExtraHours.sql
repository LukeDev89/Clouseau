-- Add ExtraHours column to TaskProgress table
ALTER TABLE TaskProgress
ADD ExtraHours DECIMAL(18,2) NOT NULL DEFAULT 0;

-- Update existing records to have 0 extra hours
UPDATE TaskProgress SET ExtraHours = 0 WHERE ExtraHours IS NULL;