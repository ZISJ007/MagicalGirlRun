# MagicalGirlRun

Unity 기반 2D Endless Runner 게임 프로젝트  
플레이어는 마법소녀가 되기 싫어서 끊임없이 달리고, 장애물을 피하며 마법소녀들에게 도망칩니다.

[게임 실행 이미지]
![image](https://github.com/user-attachments/assets/d24d6b84-076f-4684-a823-8fb59b1508c5)

![image](https://github.com/user-attachments/assets/81e4ce44-38b7-45cb-abe3-5c546818a235)

[스테이지 내에 보석을 모으면]
![image](https://github.com/user-attachments/assets/40f1801a-bd90-4c57-b376-e54def07a862)

[열쇠획득 성공]
![image](https://github.com/user-attachments/assets/d97c9cb2-ec0b-4e37-927c-7c2754184e0b)

[총 3개를 모으면 보스스테이지 열람]
![image](https://github.com/user-attachments/assets/0485db9a-635a-4df6-a62a-df8f63f044a1)

 <!-- 실제 스크린샷을 넣어주세요 -->

---

## 🎮 게임 특징

- **Endless Run**
  - 캐릭터는 자동으로 앞으로 이동
  - 플레이어는 점프 및 회피 조작만으로 난관 극복
- **장애물 회피 시스템**
  - 지상, 공중 장애물 랜덤 생성
  - 장애물 충돌 시 즉시 게임 오버
- **에너지 및 스코어**
  - 진행 거리 기반 점수 시스템
  - 획득 가능한 에너지(아이템) (확장 예정)

---

## 🎮 기본 조작법

- **점프**
  - Space
- **슬라이딩**
  - Left Shift

---

## 🛠 사용 기술 및 아키텍처
| 기술 | 설명 |
|------|------|
| Unity 2022.3.10f1 | 게임 엔진 |
| C# | 스크립트 |
| Unity Object Pooling | 오브젝트 최적화 생성 관리 |
| Physics2D | 물리 충돌 |

**핵심 아키텍처 요약**
- `PoolManager` : 장애물, 총알 등 오브젝트 풀 관리
- `ObstacleSpawner` : 장애물 랜덤 스폰 시스템
- `PlayerController` : 캐릭터 이동 및 점프 처리
- `GameManager` : 전체 게임 상태 관리

---

## 📂 프로젝트 구조
MagicalGirlRun-main/

├── Assets/

│ ├── Scripts/ # 게임 스크립트

│ ├── Prefabs/ # 프리팹 리소스

│ ├── Scenes/ # 메인 씬 포함

│ └── Art/ # 스프라이트 및 애니메이션

├── Packages/

├── ProjectSettings/

└── README.md
