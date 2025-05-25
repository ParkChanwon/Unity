
# VR/AR 게임 제작 기초 기말 프로젝트 — Survive Or Dead
<img width="1468" alt="Image" src="https://github.com/user-attachments/assets/b228e67a-204e-4384-bdbe-b6ec0ee61912" />

---

##  게임 소개

`Survive Or Dead`는 Unity 기반으로 VR/AR 게임 제작 기초를 학습하며 만든 **기말 프로젝트**입니다.  
핵심 목표는 

- 플레이어 이동 및 충돌 처리
- 힐링팩 등 오브젝트와의 상호작용  
- 간단하게 플레이할 수 있는 게임

---

##  게임 특징

-  WASD/화살표키 이동
-  3인칭 시점으로 마우스로 카메라 회전 및 발사 
-  긴박한 BGM과 사운드 이펙트

---

<img width="489" alt="Image" src="https://github.com/user-attachments/assets/0af9f45f-670b-4c20-aecd-d95feea951bc" />

위 이미지는 플레이어를 향해 다가오는 좀비와 플레이어, 그리고 오브젝트인 힐링팩입니다.
</br>

<img width="641" alt="Image" src="https://github.com/user-attachments/assets/d2493c71-ed05-4e18-af17-8cd2e7aa6404" />


게임을 진행하며 점점 좀비들이 플레이어 위치를 따라오고, 스테이지별로 시간과 좀비 스폰 시간을 다르게 하였으며,
맵 여러 곳에서 좀비가 나타나 플레이어는 이를 피해다니거나 잡으면서 주어진 시간을 버티면 다음 스테이지로 넘어가게 됩니다.
</br>

<img width="380" alt="Image" src="https://github.com/user-attachments/assets/d54e530e-bc63-455a-a067-d28205e2bd8b" />


스테이지가 거듭날수록 좀비들이 더 빨리 나오게 되며, 점점 어렵게 구성하였습니다.

</br>
<img width="1468" alt="Image" src="https://github.com/user-attachments/assets/8b71a23c-e05f-4698-9784-f69f58670e4d" />

왼쪽 위에는 현재 맵에 생성된 좀비의 수, 설정된 시간이 지나면 화면 중앙에 Stage Clear, 오른쪽 상단에는 HP바를 놓아 현재 HP를 확인할 수 있게 하였습니다.


## 핵심 코드

---
<img width="615" alt="Image" src="https://github.com/user-attachments/assets/c73fbcd2-6e25-4708-8435-701c42d1da1e" />
<img width="477" alt="Image" src="https://github.com/user-attachments/assets/31785f0e-e099-4c9b-8b50-0add44bf05e5" />
</br>
위 코드들은 좀비가 플레이어를 향해 다가오게 하고, 좀비는 계속해서 플레이어에게 다가옴으로 완전 겹쳐버리는 현상을 막기 위해 Vector값을 계산하여 막았습니다.

총알의 물리적인 충돌을 감지하면 DecreasesHP()를 호출하여 HP를 감소하였습니다.
좀비와 플레이어가 충돌이 처음에는 -10의 데미지를 입고, 이후에는 지속적으로 -2만큼의 HP가 감소하도록 하였습니다.
</br>

<img width="405" alt="Image" src="https://github.com/user-attachments/assets/17b3f910-f73d-4bf5-b169-7719567fed03" />

좀비와 힐팩을 스폰하는 과정에서 과도하게 좀비가 많이 생성되거나, 힐팩이 많이 생성되는 경우가 있어, 과도한 생성을 막고, 화면 밖에서 좀비가 뛰어올 수 있게끔 설정하였습니다.


##시연 영상
</br>
![Image](https://github.com/user-attachments/assets/82034823-d00e-4a3e-904f-f873f48aaa12)

