package com.example.user.proba;

import android.app.Service;
import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;
import android.media.MediaPlayer;
import android.os.Build;
import android.os.IBinder;
import android.support.annotation.Nullable;

import java.io.IOException;
import java.util.ArrayList;
import android.content.ContentUris;
import android.media.AudioManager;
import android.media.MediaPlayer;
import android.net.Uri;
import android.os.Binder;
import android.os.PowerManager;
import android.support.annotation.RequiresApi;
import android.util.Log;
import android.widget.Toast;
import java.util.Random;
import android.app.Notification;
import android.app.PendingIntent;




public class MusicService extends Service implements
        MediaPlayer.OnPreparedListener, MediaPlayer.OnErrorListener,
        MediaPlayer.OnCompletionListener {
    //private final static int MAX_VOLUME = 100;
    //final float minVolume = 50;
    //final float volume = (float) (1 - (Math.log(MAX_VOLUME - minVolume) / Math.log(MAX_VOLUME)));
    final String LOG_TAG = "myLogs";
        private final IBinder musicBind = new MusicBinder();
        //media player
        private MediaPlayer player;
        //song list
        private ArrayList<Track> songs;
        //current position
        private int songPosn;
        public boolean plaing;
        public boolean loopt;
        static int totalsTime;
        private String songTitle = "";
        //private static final int NOTIFY_ID=1;
        private boolean shuffle;
        private Random rand;
        int dr;


    public void onCreate(){
        //create the service
        super.onCreate();
        //initialize position
        songPosn=0;
        //create player
        player = new MediaPlayer();
        //player.setVolume(volume,volume);
        initMusicPlayer();
        plaing = false;
        totalsTime = 1;
        rand=new Random();
        shuffle=false;
        loopt=false;

    }

    public void setShuffle(boolean index){
        if(index == false) shuffle=false;
        else shuffle=true;
    }

    public void initMusicPlayer(){
        player.setWakeMode(getApplicationContext(),
                PowerManager.PARTIAL_WAKE_LOCK);
        player.setAudioStreamType(AudioManager.STREAM_MUSIC);
        player.setOnPreparedListener(this);
        player.setOnCompletionListener(this);
        player.setOnErrorListener(this);
    }

    public void setList(ArrayList<Track> theSongs){
        songs=theSongs;
    }

    public class MusicBinder extends Binder {
        MusicService getService() {
            return MusicService.this;
        }
    }

    @Nullable
    @Override
    public IBinder onBind(Intent intent) {
        return musicBind;
    }

    @Override
    public boolean onUnbind(Intent intent){
        player.stop();
        player.release();
        return false;
    }



    public void pauseSong(){
        player.pause();
        plaing = false;
    }

    @Override
    public void onCompletion(MediaPlayer mp) {
        try {
            playNext();
        } catch (IOException e) {
            e.printStackTrace();
        }
    }

    @Override
    public boolean onError(MediaPlayer mp, int what, int extra) {
        mp.reset();
        return false;
    }

    @RequiresApi(api = Build.VERSION_CODES.JELLY_BEAN)
    @Override
    public void onPrepared(MediaPlayer mp) {
        //start playback
        mp.start();
        dr = player.getDuration();

        Intent notIntent = new Intent(this, MainActivity.class);
        notIntent.addFlags(Intent.FLAG_ACTIVITY_CLEAR_TOP);
        PendingIntent pendInt = PendingIntent.getActivity(this, 0,
                notIntent, PendingIntent.FLAG_UPDATE_CURRENT);


        try {
            Notification.Builder builder = new Notification.Builder(this);

            builder.setContentIntent(pendInt)
                    .setSmallIcon(R.drawable.ic_launcher_background)
                    .setTicker(songTitle)
                    .setOngoing(true)
                    .setContentTitle("Playing")
                    .setContentText(songTitle);
            Notification not = builder.build();

            startForeground(1, not);
        }
        catch (Exception e){

        }
    }

    public void loop(boolean Index){
        loopt = Index;
        if(loopt==true) player.setLooping(true);
        else player.setLooping(false);
    }

    public void setSong(int songIndex){
        songPosn=songIndex;
    }

    public String SetTitle(){
        return songTitle;
    }

    public boolean GetPlaing(){
        return plaing;
    }

    public int setTime(){

        return player.getDuration();
    }




    public void playSong() throws IOException {
        Log.d(LOG_TAG, "Номер:1");
        player.reset();
        Log.d(LOG_TAG, "Номер:2 - " + songPosn + " songs.size = "+ songs.size());
        Track SongsPlay = songs.get(songPosn);
        Log.d(LOG_TAG, "Номер:3");
        Log.d(LOG_TAG, SongsPlay.getArtist());
        //get id
        Log.d(LOG_TAG, "Номер:4");
        long currSong = SongsPlay.getID();
        Log.d(LOG_TAG, "Номер: "+ currSong);
        //set uri
        Uri trackUri = ContentUris.withAppendedId(
                android.provider.MediaStore.Audio.Media.EXTERNAL_CONTENT_URI,
                currSong);
        try{
            player.setDataSource(getApplicationContext(), trackUri);
            player.prepare();
            //player.prepareAsync();
            songTitle=SongsPlay.getTitle();
        }
        catch(IllegalStateException e){

            player = new MediaPlayer();
            player.setDataSource(getApplicationContext(), trackUri);
            player.prepare();
            player.setOnPreparedListener(new MediaPlayer.OnPreparedListener(){
                @Override
                public void onPrepared(MediaPlayer mp){
                    mp.start();
                }
            });
        }
        go();
     }

    public int getPosn(){
        return player.getCurrentPosition();
    }

    public int getDur(){
        return dr;//player.getDuration();
    }

    public boolean isPng(){
        return player.isPlaying();
    }

    public void pausePlayer(){
        player.pause();
    }

    public void seek(int posn){
        player.seekTo(posn);
    }

    public void go(){
        player.start();
        plaing = true;
    }

    public void playPrev() throws IOException {

        if(songPosn-1<0) {
            songPosn = songs.size() - 1;
            player.reset();
            playSong();
        }else
            {
                player.reset();
                songPosn = songPosn - 1;
                playSong();
            }

    }



    //skip to next
    public void playNext() throws IOException {

            if (shuffle) {
                player.reset();
                int newSong = songPosn;
                while (newSong == songPosn) {
                    newSong = rand.nextInt(songs.size());
                }
                songPosn = newSong;

            } else {
                player.reset();
                if (songPosn + 1 >= songs.size()) songPosn = 0;
                else songPosn = songPosn + 1;

            }
            playSong();

    }
    @Override
    public void onDestroy() {
        player.stop();
        //stopForeground(true);
    }

    public void stop(){
        player.stop();
    }

}
