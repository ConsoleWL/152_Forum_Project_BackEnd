CREATE TABLE `Direct Message`(
    `Id` INT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY,
    `FromUserId` INT NOT NULL,
    `Text` VARCHAR(255) NOT NULL,
    `ToUserId` INT NOT NULL
);
ALTER TABLE
    `Direct Message` ADD UNIQUE `direct message_fromuserid_unique`(`FromUserId`);
ALTER TABLE
    `Direct Message` ADD UNIQUE `direct message_touserid_unique`(`ToUserId`);
CREATE TABLE `Comment`(
    `CommentId` INT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY,
    `UserId` INT NOT NULL,
    `TimePosted` DATETIME NOT NULL,
    `Likes` INT NOT NULL,
    `Text` VARCHAR(255) NOT NULL,
    `TopicId` BIGINT NOT NULL
);
ALTER TABLE
    `Comment` ADD UNIQUE `comment_userid_unique`(`UserId`);
ALTER TABLE
    `Comment` ADD UNIQUE `comment_topicid_unique`(`TopicId`);
CREATE TABLE `User`(
    `Id` INT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY,
    `FirstName` VARCHAR(255) NOT NULL,
    `LastName` VARCHAR(255) NOT NULL,
    `Email` VARCHAR(255) NOT NULL,
    `Password` VARCHAR(255) NOT NULL,
    `Likes` INT NOT NULL,
    `Messages` INT NOT NULL,
    `Registration Date` DATETIME NOT NULL,
    `Profile Picture` VARCHAR(255) NOT NULL,
    `UserName` VARCHAR(255) NOT NULL
);
CREATE TABLE `Topics`(
    `TopicId` BIGINT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY,
    `Text` VARCHAR(255) NOT NULL,
    `UserId` INT NOT NULL,
    `TimePosted` DATETIME NOT NULL,
    `Likes` INT NOT NULL
);
ALTER TABLE
    `Topics` ADD UNIQUE `topics_userid_unique`(`UserId`);
ALTER TABLE
    `Direct Message` ADD CONSTRAINT `direct message_touserid_foreign` FOREIGN KEY(`ToUserId`) REFERENCES `User`(`Id`);
ALTER TABLE
    `Comment` ADD CONSTRAINT `comment_userid_foreign` FOREIGN KEY(`UserId`) REFERENCES `User`(`Id`);
ALTER TABLE
    `Comment` ADD CONSTRAINT `comment_topicid_foreign` FOREIGN KEY(`TopicId`) REFERENCES `Topics`(`TopicId`);
ALTER TABLE
    `Direct Message` ADD CONSTRAINT `direct message_fromuserid_foreign` FOREIGN KEY(`FromUserId`) REFERENCES `User`(`Id`);
ALTER TABLE
    `Topics` ADD CONSTRAINT `topics_userid_foreign` FOREIGN KEY(`UserId`) REFERENCES `User`(`Id`);