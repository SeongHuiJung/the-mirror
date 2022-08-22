using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FormalBeliverScript : PlayerControllerScript
{
    // Start is called before the first frame update
    void Awake()
    {
        corrector = new ScreenCoordinateCorrector();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteWidthInUnit = corrector.convertToUnit(spriteRenderer.sprite.rect.size.x);
        spriteHeightInUnit = corrector.convertToUnit(spriteRenderer.sprite.rect.size.y);
        playerAnimationController = GetComponent<Animator>();
        speed = new Vector3(0, 0, 0);
    }

     //ActiveMove(1, 1, false); // �̵��Ҷ� �� �Լ� ���
}
