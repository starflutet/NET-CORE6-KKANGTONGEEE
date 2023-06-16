# .NET CORE 6 Dapper 사용예시 API 프로젝트입니다.
Lenguage : .NET CORE 6 ( C# )

DB : Oracle - Procedure Use ( Dapper )

## 프로젝트 구조

	PROJECT
      => Source
        => App
        => Controllers
          => Service
          => Adaptor          
        => DSerialize
        => Models
        => Serialize

## App

Program.cs, 상수값( API KEY 등 ) 등의 세팅과 관련된 소스들을 넣고있습니다.

## Controllers

URL 매핑할 컨트롤러입니다.

## Controllers => Service

DB기능을 제외한 모든 기능을 관리합니다.
명칭은 이해하기쉽게 S를 붙여주고있습니다. 

## Controllers => Adaptor

DB 와 관련된 기능들만 관리합니다.
명칭은 이해하기쉽게 A를 붙여주고있습니다. 

## DSerialize ( Request )

사용자의 요청값을 커스텀/제한 주고싶을때가 있습니다. 그럴때 사용하는 용도이며,
명칭은 이해하기쉽게 Req를 붙여주고있습니다. 
    
## Serialize ( Response )

서버의 응답값을 커스텀/제한 주고싶을때가 있습니다. 그럴때 사용하는 용도이며,
명칭은 이해하기쉽게 Res를 붙여주고있습니다. 

## Models 

직접적으로 디비와 매핑하기 위한 용도이며, 변수명을 반드시 디비 컬럼명과 동일하게 지정해야합니다. ( 대소문자, _ , - 등 포함 )
JsonProperty 으로 클라이언트에게 전달할 Json 커스텀이 가능합니다.

## 끝 

개발에 정답은 없으므로 입맛데로 주물주물 하세요 ㅎ
