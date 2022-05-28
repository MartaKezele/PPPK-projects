package hr.algebra.personmanager.dao

import android.content.Context
import androidx.room.Database
import androidx.room.Room
import androidx.room.RoomDatabase
import androidx.room.TypeConverters
import androidx.room.migration.Migration
import androidx.sqlite.db.SupportSQLiteDatabase

val MIGRATION_1_2 = object : Migration(1, 2) {
    override fun migrate(database: SupportSQLiteDatabase) {
        // create new table
        database.execSQL("CREATE TABLE IF NOT EXISTS `people_new` (`_id` INTEGER PRIMARY KEY AUTOINCREMENT, `title` TEXT, `firstName` TEXT, `lastName` TEXT, `picturePath` TEXT, `birthDate` INTEGER NOT NULL)")
        // copy data to new table
        database.execSQL("INSERT INTO `people_new` (`firstName`, `lastName`, `picturePath`, `birthDate`) SELECT `firstName`, `lastName`, `picturePath`, `birthDate` FROM `people`")
        // remove the old table
        database.execSQL("DROP TABLE `people`")
        // rename new table
        database.execSQL("ALTER TABLE `people_new` RENAME TO `people`")
    }
}

@Database(entities = [Person::class], version = 2)
@TypeConverters(DateConverter::class)
abstract class PeopleDatabase : RoomDatabase() {
    abstract fun personDao() : PersonDao

    companion object {
        @Volatile private var INSTANCE: PeopleDatabase? = null
        fun getInstance(context: Context) =
            INSTANCE ?: synchronized(PeopleDatabase::class.java) {
                INSTANCE ?: buildDatabase(context).also { INSTANCE = it }
            }


        private fun buildDatabase(context: Context) = Room.databaseBuilder(
            context.applicationContext,
            PeopleDatabase::class.java,
            "people.db"
        )
            .addMigrations(MIGRATION_1_2)
            .build()
    }
}