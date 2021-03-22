// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleMigrations;

namespace Jeebs.Auth.Data.Clients.MySql.Migrations
{
	[Migration(1, "Add user table")]
	public sealed class AddUserTable : Migration
	{
		protected override void Up()
		{
			Execute(@"
				CREATE TABLE `auth_user` (
					`UserId` INT(11) NOT NULL AUTO_INCREMENT,
					`UserVersion` INT(11) NOT NULL DEFAULT '0',
					`UserEmailAddress` VARCHAR(128) NOT NULL COLLATE 'utf8_general_ci',
					`UserPasswordHash` VARCHAR(128) NOT NULL COLLATE 'utf8_general_ci',
					`UserFriendlyName` VARCHAR(32) NOT NULL COLLATE 'utf8_general_ci',
					`UserGivenName` VARCHAR(128) NULL DEFAULT NULL COLLATE 'utf8_general_ci',
					`UserFamilyName` VARCHAR(128) NULL DEFAULT NULL COLLATE 'utf8_general_ci',
					`UserIsEnabled` BIT(1) NOT NULL DEFAULT b'0',
					`UserIsSuper` BIT(1) NOT NULL DEFAULT b'0',
					`UserLastSignedIn` DATETIME NULL DEFAULT NULL,
					PRIMARY KEY (`UserId`) USING BTREE,
					UNIQUE INDEX `UserEmailAddress` (`UserEmailAddress`) USING BTREE
				)
				COLLATE='utf8_general_ci'
				ENGINE=InnoDB
				;
			");
		}

		protected override void Down()
		{
			Execute("DROP TABLE `auth_user`;");
		}
	}
}
