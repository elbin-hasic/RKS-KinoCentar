package com.example.kinocentar.helper;

import android.util.Base64;

import java.io.UnsupportedEncodingException;
import java.security.MessageDigest;
import java.security.NoSuchAlgorithmException;
import java.security.SecureRandom;

public class MyCryption
{
    public static String GenerateBase64(String text) {
        try {
            return Base64.encodeToString(text.getBytes("UTF-8"), Base64.DEFAULT);
        }
        catch (Exception e) {
            return "";
        }
    }

    public static String ByteToString(byte[] input) {
        return Base64.encode(input, Base64.NO_WRAP).toString();
    }

    public static byte[] GenerateSalt() {
        SecureRandom random = new SecureRandom();
        byte bytes[] = new byte[16];
        random.nextBytes(bytes);
        return bytes;
    }

    public static String GenerateHash(String password) throws NoSuchAlgorithmException, UnsupportedEncodingException
    {
        MessageDigest md = MessageDigest.getInstance("SHA-1");
        byte[] textBytes = password.getBytes("UTF-8");
        md.update(textBytes, 0, textBytes.length);

        byte[] hashedBytes = md.digest();
        return ByteToString(hashedBytes);
    }

    public static String GenerateHash(String password, String salt) throws NoSuchAlgorithmException, UnsupportedEncodingException {
        byte[] passwordBytes = password.getBytes("UTF-8");
        byte[] saltBytes = Base64.decode(salt, Base64.NO_WRAP);

        MessageDigest digest = MessageDigest.getInstance("SHA-1");
        digest.reset();
        digest.update(saltBytes);

        byte[] hashedBytes = digest.digest(passwordBytes);
        return ByteToString(hashedBytes);
    }
}
