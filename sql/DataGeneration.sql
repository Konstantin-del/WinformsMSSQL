-- INSERT INTO dbo.status 

INSERT INTO dbo.status (name) VALUES ('Active');

INSERT INTO dbo.status (name) VALUES ('On leave');

INSERT INTO dbo.status (name) VALUES ('Fired');

-- INSERT INTO dbo.posts

INSERT INTO dbo.posts (name) VALUES ('Developer');

INSERT INTO dbo.posts (name) VALUES ('Manager');

INSERT INTO dbo.posts (name) VALUES ('HR');

-- INSERT INTO dbo.deps

INSERT INTO dbo.deps (name) VALUES ('IT');

INSERT INTO dbo.deps (name) VALUES ('Sales');

INSERT INTO dbo.deps (name) VALUES ('HR Department');

-- INSERT INTO dbo.persons

BEGIN
    DECLARE @status_id INT = (SELECT id FROM dbo.status WHERE name = 'Active');
    DECLARE @dep_id INT = (SELECT id FROM dbo.deps WHERE name = 'IT');
    DECLARE @post_id INT = (SELECT id FROM dbo.posts WHERE name = 'Developer');

    INSERT INTO dbo.persons (first_name, second_name, last_name, date_employ, date_uneploy, status_id, dep_id, post_id)
    VALUES ('Ivan', 'Sidorovich', 'Petrov', '2020-03-15', NULL, @status_id, @dep_id, @post_id);
END

BEGIN
    DECLARE @status_id INT = (SELECT id FROM dbo.status WHERE name = 'Active');
    DECLARE @dep_id INT = (SELECT id FROM dbo.deps WHERE name = 'HR Department');
    DECLARE @post_id INT = (SELECT id FROM dbo.posts WHERE name = 'Developer');

    INSERT INTO dbo.persons (first_name, second_name, last_name, date_employ, date_uneploy, status_id, dep_id, post_id)
    VALUES ('Anton', 'Alecseevich', 'Alecseev', '2020-03-15', NULL, @status_id, @dep_id, @post_id);
END

BEGIN
    DECLARE @status_id2 INT = (SELECT id FROM dbo.status WHERE name = 'On leave');
    DECLARE @dep_id2 INT = (SELECT id FROM dbo.deps WHERE name = 'HR Department');
    DECLARE @post_id2 INT = (SELECT id FROM dbo.posts WHERE name = 'HR');

    INSERT INTO dbo.persons (first_name, second_name, last_name, date_employ, date_uneploy, status_id, dep_id, post_id)
    VALUES ('Anna', 'Sergeevna', 'Ivanova', '2019-06-01', NULL, @status_id2, @dep_id2, @post_id2);
END

BEGIN
    DECLARE @status_id3 INT = (SELECT id FROM dbo.status WHERE name = 'Fired');
    DECLARE @dep_id3 INT = (SELECT id FROM dbo.deps WHERE name = 'Sales');
    DECLARE @post_id3 INT = (SELECT id FROM dbo.posts WHERE name = 'Manager');

    INSERT INTO dbo.persons (first_name, second_name, last_name, date_employ, date_uneploy, status_id, dep_id, post_id)
    VALUES ('Petr', 'Alexeevich', 'Sidorov', '2015-01-10', '2021-09-30', @status_id3, @dep_id3, @post_id3);
END